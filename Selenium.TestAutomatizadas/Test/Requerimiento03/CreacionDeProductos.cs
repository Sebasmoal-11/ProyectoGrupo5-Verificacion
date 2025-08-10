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
            using var driver = new ChromeDriver(options);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var page = new ProductoPage(driver);

            // 1) Ir a index y esperar enlace "Agregar Producto"
            page.IrAIndex(_baseUrl);
            wait.Until(d => d.FindElement(By.LinkText("Agregar Producto")).Displayed);
            page.ClickAgregarProducto();

            // 2) Esperar el formulario y completarlo
            wait.Until(d => d.FindElement(By.Id("productName")).Displayed);

            string nombre = $"Producto QA {DateTime.UtcNow:HHmmss}";
            string descripcion = "Descripción de prueba automática";
            string precio = "19.99";

            page.CompletarFormulario(nombre, descripcion, precio);
            page.EnviarFormularioProducto();

            // 3) Verificar que aparece en la lista
            By listaProductos = By.CssSelector("table#products, ul#products, .products-list");
            wait.Until(d => d.FindElements(listaProductos).Count > 0);

            var xpath = $"//*[self::td or self::li or self::a][contains(normalize-space(.), '{nombre}')]";
            bool encontrado = false;

            try
            {
                encontrado = wait.Until(d => d.FindElement(By.XPath(xpath)).Displayed);
            }
            catch
            {
                // Intento extra: ir a index y buscar de nuevo
                page.IrAIndex(_baseUrl);
                wait.Until(d => d.FindElements(listaProductos).Count > 0);
                try
                {
                    encontrado = wait.Until(d => d.FindElement(By.XPath(xpath)).Displayed);
                }
                catch
                {
                    encontrado = false;
                }
            }

            if (!encontrado)
                throw new Exception($"[Productos] '{nombre}' no aparece en la lista.");

            Console.WriteLine($"[Productos] ✅ '{nombre}' creado y visible en la lista.");
        }
    }
}


