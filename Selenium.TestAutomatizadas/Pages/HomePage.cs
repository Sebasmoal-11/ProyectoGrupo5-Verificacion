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

        // TODO: ajusta a un elemento estable de la Home
        private By HomeMarker => By.CssSelector("header .logo, .home-hero, #home-marker");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public TimeSpan NavegarYMedirCarga(string baseUrl)
        {
            var sw = Stopwatch.StartNew();
            _driver.Navigate().GoToUrl(baseUrl);
            _wait.Until(ExpectedConditions.ElementIsVisible(HomeMarker));
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
