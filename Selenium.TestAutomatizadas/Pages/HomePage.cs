using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Marcador real en tu index.html (ajústalo si el texto cambia)
        private By HomeMarker => By.LinkText("Agregar Producto");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public TimeSpan NavegarYMedirCarga(string baseUrl)
        {
            var sw = Stopwatch.StartNew();
            _driver.Navigate().GoToUrl(baseUrl);

            // Documento listo (evitamos warning con null-forgiving)
            _wait.Until(_ =>
                ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState")!
                    .ToString() == "complete");

            // Marcador visible (sin ExpectedConditions)
            _wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(HomeMarker);
                    return el.Displayed;
                }
                catch (NoSuchElementException) { return false; }
                catch (StaleElementReferenceException) { return false; }
            });

            sw.Stop();
            return sw.Elapsed;
        }
    }
}

