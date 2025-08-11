using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.TestAutomatizadas.Pages;

namespace Selenium.TestAutomatizadas.Test.Requerimiento03
{
    public class CreacionDeProductos
    {
        private readonly string _baseUrl;
        public CreacionDeProductos(string baseUrl) => _baseUrl = baseUrl;

        public void Ejecutar()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept; // auto-aceptar alerts

            using var driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(12));
            var page = new ProductoPage(driver); // ← TU Page, sin cambios

            // 1) Ir a index y esperar
            page.IrAIndex(_baseUrl);
            EsperarDocumentoListo(driver, wait);

            // 2) Click en "Agregar Producto"
            wait.Until(d => { try { return d.FindElement(By.LinkText("Agregar Producto")).Displayed; } catch { return false; } });
            page.ClickAgregarProducto();

            // 3) Esperar el form (#productName) y completar
            wait.Until(d => { try { return d.FindElement(By.Id("Name")).Displayed; } catch { return false; } });

            string nombre = $"Producto QA {DateTime.UtcNow:HHmmss}";
            page.CompletarFormulario(nombre, "Descripción de prueba automática", "19");
            page.EnviarFormularioProducto();

            // 4) Aceptar alert si lo hay
            TryAcceptAlert(driver, 2);

            // 5) Volver/asegurar index
            page.IrAIndex(_baseUrl);
            EsperarDocumentoListo(driver, wait);

            // 6) Buscar en UI
            bool encontradoUI = BuscarProductoEnContenedores(driver, nombre) || BodyContieneTexto(driver, nombre);
            // 6) Esperar hasta que aparezca el producto en la lista
            // Esperar a que aparezca el producto en la lista, usando wait
            try
            {
                wait.Until(d => d.FindElements(By.XPath($"//div[contains(@class, 'product')]//h3[contains(text(), '{nombre}')]")).Count > 0);
                encontradoUI = true;
            }
            catch (WebDriverTimeoutException)
            {
                encontradoUI = BuscarProductoEnContenedores(driver, nombre) || BodyContieneTexto(driver, nombre);
            }

            // Validación con llaves para evitar ambigüedades
            if (!encontradoUI)
            {
                Console.WriteLine($"DEBUG - Nombre del producto: '{nombre ?? "null"}'");
                throw new Exception($"[Productos] '{nombre ?? "null"}' no aparece en la lista.");
            }

            Console.WriteLine($"[Productos] ✅ '{nombre}' creado y visible en la lista.");




            if (!encontradoUI)
            {
                // 7) Validar en localStorage (muchas demos guardan ahí)
                bool enStorage = ExisteEnLocalStorage(driver, nombre);

                if (enStorage)
                {
                    Console.WriteLine("[Productos] ⚠️ No se ve en la lista todavía, pero sí está en localStorage. Refrescando…");
                    driver.Navigate().Refresh();
                    EsperarDocumentoListo(driver, wait);
                    encontradoUI = BuscarProductoEnContenedores(driver, nombre) || BodyContieneTexto(driver, nombre);

                    if (!encontradoUI)
                    {
                        // Lo damos por válido: la UI no refresca automáticamente
                        Console.WriteLine($"[Productos] ✅ '{nombre}' creado (validado por localStorage). La UI no lo muestra aún.");
                        return;
                    }
                }
                else
                {
                    // Dump útil para diagnosticar
                    try
                    {
                        Console.WriteLine("[DEBUG] URL: " + driver.Url);
                        Console.WriteLine("[DEBUG] ¿Hay contenedores de lista? " + HayLista(driver));
                        var body = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body && document.body.innerText || ''")?.ToString() ?? "";
                        Console.WriteLine("[DEBUG] body contiene el nombre? " + (body.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0 ? "sí" : "no"));
                    }
                    catch { }
                }
            }
        }

        // ========= Helpers =========

        private static void EsperarDocumentoListo(IWebDriver driver, WebDriverWait wait)
        {
            wait.Until(_ =>
                ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState")!
                    .ToString() == "complete");
        }

        private static bool TryAcceptAlert(IWebDriver driver, int seconds)
        {
            var w = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            try
            {
                IAlert? alert = w.Until(d =>
                {
                    try { return d.SwitchTo().Alert(); }
                    catch (NoAlertPresentException) { return null; }
                });
                if (alert == null) return false;
                try { alert.Accept(); } catch { }
                return true;
            }
            catch { return false; }
        }

        private static bool HayLista(IWebDriver d)
        {
            foreach (var by in ContenedoresLista())
                if (d.FindElements(by).Count > 0) return true;
            return false;
        }

        private static bool BuscarProductoEnContenedores(IWebDriver driver, string nombre)
        {
            foreach (var by in ContenedoresLista())
            {
                var conts = driver.FindElements(by);
                foreach (var c in conts)
                {
                    var items = c.FindElements(By.XPath($".//*[contains(normalize-space(.), '{nombre}')]"));
                    if (items.Count > 0) return true;
                }
            }
            // Cards sueltas (Bootstrap, etc.)
            var cards = driver.FindElements(By.CssSelector(".card, .product, .item, .tile"));
            foreach (var card in cards)
            {
                var t = card.Text?.Trim() ?? "";
                if (t.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            }
            return false;
        }

        private static By[] ContenedoresLista() => new[]
        {
            By.CssSelector("table#products"),
            By.Id("products"),
            By.CssSelector("ul#products"),
            By.CssSelector(".products-list"),
            By.Id("listaProductos"),
            By.Id("product-list"),
            By.CssSelector(".product-list"),
            By.CssSelector(".cards"),
            By.CssSelector(".grid-products"),
            // Fallbacks genéricos
            By.CssSelector("table"),
            By.CssSelector("ul"),
            By.CssSelector(".list-group")
        };

        private static bool BodyContieneTexto(IWebDriver driver, string texto)
        {
            try
            {
                var body = ((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.body && document.body.innerText || ''")
                    ?.ToString() ?? "";
                return body.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            catch { return false; }
        }

        private static bool ExisteEnLocalStorage(IWebDriver driver, string nombre)
        {
            try
            {
                var js = (IJavaScriptExecutor)driver;
                var foundObj = js.ExecuteScript(@"
                    var needle = arguments[0].toLowerCase();
                    // 1) Intenta keys típicas con array JSON
                    var candidatos = ['products','productos','listaProductos','productList'];
                    for (var c of candidatos){
                        try{
                            var raw = localStorage.getItem(c);
                            if (!raw) continue;
                            var arr = JSON.parse(raw);
                            if (Array.isArray(arr)){
                                for (var i=0;i<arr.length;i++){
                                    var p = arr[i] || {};
                                    var txt = (p.name || p.nombre || p.title || JSON.stringify(p) || '').toLowerCase();
                                    if (txt.indexOf(needle) !== -1) return true;
                                }
                            }
                        }catch(e){}
                    }
                    // 2) Búsqueda global por si el valor no es JSON
                    for (var i=0;i<localStorage.length;i++){
                        var k = localStorage.key(i);
                        var v = (localStorage.getItem(k) || '').toLowerCase();
                        if (v.indexOf(needle) !== -1) return true;
                    }
                    return false;
                ", nombre);
                return foundObj is bool b && b;
            }
            catch { return false; }
        }
    }
}
