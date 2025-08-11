using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Pages
{
    public class ContactoPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private By? _byNombre;
        private By? _byCorreo;
        private By? _byMensaje;

        private By _byEnviar = By.CssSelector("form#contact button[type='submit']");
        private By _byExito = By.CssSelector(".alert.alert-success, .toast-success, #contact-success");

        public ContactoPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public ContactoPage Ir(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl);

            // Intentar menú Contacto/Contact
            var links = new[]
            {
                By.LinkText("Contacto"),
                By.PartialLinkText("Contacto"),
                By.LinkText("Contact"),
                By.PartialLinkText("Contact"),
            };
            foreach (var by in links)
            {
                var encontrados = _driver.FindElements(by);
                if (encontrados.Count > 0) { encontrados[0].Click(); break; }
            }

            // Anchors comunes
            if (!DetectarCampos(2))
            {
                foreach (var a in new[] { "#contact", "#contacto", "#Contact", "#Contacto" })
                {
                    _driver.Navigate().GoToUrl(AppendAnchor(baseUrl, a));
                    if (DetectarCampos(2)) break;
                }
            }

            // contact.html / contacto.html
            if (_byNombre is null)
            {
                foreach (var url in GuessContactPages(baseUrl))
                {
                    _driver.Navigate().GoToUrl(url);
                    if (DetectarCampos(5)) break;
                }
            }

            if (_byNombre is null || _byCorreo is null || _byMensaje is null)
                throw new WebDriverTimeoutException(
                    "No se encontró el formulario de Contacto. Verifica ruta/IDs en tu HTML.");

            return this;
        }

        public ContactoPage LlenarObligatorios(string nombre, string correo, string mensaje)
        {
            var byNombre = _byNombre ?? throw new InvalidOperationException("Llama primero a Ir(baseUrl).");
            var byCorreo = _byCorreo ?? throw new InvalidOperationException("Llama primero a Ir(baseUrl).");
            var byMensaje = _byMensaje ?? throw new InvalidOperationException("Llama primero a Ir(baseUrl).");

            _driver.FindElement(byNombre).Clear(); _driver.FindElement(byNombre).SendKeys(nombre);
            _driver.FindElement(byCorreo).Clear(); _driver.FindElement(byCorreo).SendKeys(correo);
            _driver.FindElement(byMensaje).Clear(); _driver.FindElement(byMensaje).SendKeys(mensaje);
            return this;
        }

        public ContactoPage EnviarFormulario()
        {
            // Hook: capturar texto de alert por JS antes de hacer clic
            try
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript(@"
                    (function(){
                        try{
                            window.__lastAlert = null;
                            var _oldAlert = window.alert;
                            window.alert = function(msg){
                                try{ window.__lastAlert = String(msg); }catch(e){}
                                try{ if (_oldAlert) _oldAlert(msg); }catch(e){}
                            };
                        }catch(e){}
                    })();
                ");
            }
            catch { /* no pasa nada si falla */ }

            if (_driver.FindElements(_byEnviar).Count == 0)
            {
                var candidatosEnviar = new[]
                {
                    By.CssSelector("button[type='submit']"),
                    By.CssSelector("input[type='submit']"),
                    By.Id("enviar"),
                    By.Id("btnEnviar"),
                    By.CssSelector("form button"),
                };
                var nuevo = PickFirst(candidatosEnviar);
                if (nuevo is not null) _byEnviar = nuevo;
            }

            _driver.FindElement(_byEnviar).Click();
            return this;
        }

        public bool SeMostroExito()
        {
            // 1) ¿Tenemos texto de alert capturado por JS?
            try
            {
                var msgObj = ((IJavaScriptExecutor)_driver).ExecuteScript("return window.__lastAlert || null;");
                var msg = msgObj?.ToString();
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    var t = msg.ToLowerInvariant();
                    if (t.Contains("éxito") || t.Contains("exito") || t.Contains("enviado"))
                        return true;
                }
            }
            catch { /* ignorar */ }

            // 2) Intento clásico: si el alert sigue abierto, tomarlo y cerrarlo
            if (TryWaitAndAcceptAlert(out var alertText))
            {
                if (!string.IsNullOrWhiteSpace(alertText))
                {
                    var t = alertText.ToLowerInvariant();
                    if (t.Contains("éxito") || t.Contains("exito") || t.Contains("enviado"))
                        return true;
                }
            }

            // 3) Fallback: buscar mensaje de éxito en el DOM
            if (_driver.FindElements(_byExito).Count == 0)
            {
                var candidatosExito = new[]
                {
                    By.CssSelector(".alert-success"),
                    By.CssSelector(".success, .is-success, .toast-success"),
                    By.Id("contact-success"),
                    By.CssSelector("[role='alert']"),
                };
                var nuevo = PickFirst(candidatosExito);
                if (nuevo is not null) _byExito = nuevo;
            }

            try
            {
                return _wait.Until(d =>
                {
                    try { return d.FindElement(_byExito).Displayed; }
                    catch (NoSuchElementException) { return false; }
                    catch (StaleElementReferenceException) { return false; }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        // ================== Helpers ==================

        private bool DetectarCampos(int seconds)
        {
            var w = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

            var nombreCands = new[]
            {
                By.Id("contact-name"), By.Id("name"), By.Name("name"), By.Id("nombre"), By.Name("nombre"),
                By.CssSelector("input[placeholder*='Nombre' i]"),
            };
            var correoCands = new[]
            {
                By.Id("contact-email"), By.Id("email"), By.Name("email"), By.Id("correo"), By.Name("correo"),
                By.CssSelector("input[type='email']"),
                By.CssSelector("input[placeholder*='Correo' i], input[placeholder*='Email' i]"),
            };
            var mensajeCands = new[]
            {
                By.Id("contact-message"), By.Id("message"), By.Name("message"),
                By.Id("mensaje"), By.Name("mensaje"),
                By.CssSelector("textarea"),
                By.CssSelector("textarea[placeholder*='Mensaje' i], textarea[placeholder*='Message' i]"),
            };

            _byNombre = PickFirstVisible(w, nombreCands);
            _byCorreo = PickFirstVisible(w, correoCands);
            _byMensaje = PickFirstVisible(w, mensajeCands);

            return _byNombre is not null && _byCorreo is not null && _byMensaje is not null;
        }

        private By? PickFirst(By[] cands)
        {
            foreach (var by in cands)
                if (_driver.FindElements(by).Count > 0)
                    return by;
            return null;
        }

        private By? PickFirstVisible(WebDriverWait w, By[] cands)
        {
            foreach (var by in cands)
            {
                try
                {
                    var ok = w.Until(d =>
                    {
                        try { return d.FindElement(by).Displayed; }
                        catch (NoSuchElementException) { return false; }
                        catch (StaleElementReferenceException) { return false; }
                    });
                    if (ok) return by;
                }
                catch (WebDriverTimeoutException) { /* siguiente */ }
            }
            return null;
        }

        private static string AppendAnchor(string baseUrl, string anchor)
        {
            var hashIdx = baseUrl.IndexOf('#');
            if (hashIdx >= 0) baseUrl = baseUrl.Substring(0, hashIdx);
            return baseUrl + anchor;
        }

        private static string[] GuessContactPages(string baseUrl)
        {
            var root = baseUrl;
            if (baseUrl.EndsWith("index.html", StringComparison.OrdinalIgnoreCase))
                root = baseUrl.Substring(0, baseUrl.Length - "index.html".Length);
            if (!root.EndsWith("/")) root += "/";

            return new[]
            {
                root + "contact.html",
                root + "contacto.html",
                root + "Contact.html",
                root + "Contacto.html",
            };
        }

        private bool TryWaitAndAcceptAlert(out string? text, int seconds = 3)
        {
            text = null;
            var w = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            try
            {
                IAlert? alert = w.Until(d =>
                {
                    try { return d.SwitchTo().Alert(); }
                    catch (NoAlertPresentException) { return null; }
                });

                if (alert is null) return false;

                text = alert.Text;
                try { alert.Accept(); } catch { /* noop */ }
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        internal bool ExisteMensajeError(string v)
        {
            throw new NotImplementedException();
        }
    }
}
