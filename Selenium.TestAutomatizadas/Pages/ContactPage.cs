using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Pages
{
    public class ContactoPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // TODO: ajusta selectores/ruta
        private By Nombre => By.Id("contact-name");
        private By Correo => By.Id("contact-email");
        private By Mensaje => By.Id("contact-message");
        private By Enviar => By.CssSelector("form#contact button[type='submit']");
        private By Exito => By.CssSelector(".alert.alert-success, .toast-success, #contact-success");

        public ContactoPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public ContactoPage Ir(string baseUrl)
        {
            _driver.Navigate().GoToUrl($"{baseUrl}/contact");
            _wait.Until(ExpectedConditions.ElementIsVisible(Nombre));
            return this;
        }

        public ContactoPage LlenarObligatorios(string nombre, string correo, string mensaje)
        {
            _driver.FindElement(Nombre).Clear(); _driver.FindElement(Nombre).SendKeys(nombre);
            _driver.FindElement(Correo).Clear(); _driver.FindElement(Correo).SendKeys(correo);
            _driver.FindElement(Mensaje).Clear(); _driver.FindElement(Mensaje).SendKeys(mensaje);
            return this;
        }

        public ContactoPage EnviarFormulario() { _driver.FindElement(Enviar).Click(); return this; }

        public bool SeMostroExito()
        {
            try { return _wait.Until(ExpectedConditions.ElementIsVisible(Exito)).Displayed; }
            catch { return false; }
        }
    }
}
