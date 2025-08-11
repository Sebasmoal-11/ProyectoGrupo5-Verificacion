using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Test.Requerimiento05 // Req 5
{
    public class NavegacionEntrePaginasTest
    {
        private readonly string _baseUrl;

        public NavegacionEntrePaginasTest(string baseUrl) => _baseUrl = baseUrl;

        public void Ejecutar()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            using var driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Navigate().GoToUrl(_baseUrl);
            Console.WriteLine("Título actual: " + driver.Title);

            // espera a que el título contenga la palabra que corresponda a la página de inicio
            Console.WriteLine("[Navegación] Página de inicio cargada.");

            wait.Until(d => !string.IsNullOrEmpty(d.Title));
            Console.WriteLine("Título tras carga: " + driver.Title);

            // Productos
            driver.FindElement(By.LinkText("Productos")).Click();
            Console.WriteLine("Título tras click en Productos: " + driver.Title);

            wait.Until(d => d.Title.Contains("Productos"));
            Console.WriteLine("[Navegación] Página de productos cargada.");

            // Contacto
            driver.FindElement(By.LinkText("Contacto")).Click();
            Console.WriteLine("Título tras click en Contacto: " + driver.Title);
            wait.Until(d => d.Title.Contains("Contacto"));
            Console.WriteLine("[Navegación] Página de contacto cargada.");
        }
    }
}
