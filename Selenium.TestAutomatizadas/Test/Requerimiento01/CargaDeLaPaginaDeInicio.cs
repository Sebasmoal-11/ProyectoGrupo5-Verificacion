using System;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;
using OpenQA.Selenium;

namespace Selenium.TestAutomatizadas.Test.Requerimiento01
{
    public class CargaDeLaPaginaDeInicio
    {
        private readonly string _baseUrl;
        public CargaDeLaPaginaDeInicio(string baseUrl) => _baseUrl = baseUrl;

        public void Ejecutar()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept; // auto-aceptar alerts

            using var driver = new ChromeDriver(options);

            var home = new HomePage(driver);

            // Simulamos 3 accesos (usuarios distintos)
            for (int i = 1; i <= 3; i++)
            {
                var t = home.NavegarYMedirCarga(_baseUrl);
                Console.WriteLine($"[Carga Home] Intento {i} -> {t.TotalMilliseconds:F0} ms");
                if (t.TotalSeconds > 3.0)
                    throw new Exception($"[Carga Home] Excedido: {t.TotalSeconds:F2}s (>3s)");
            }

            Console.WriteLine("[Carga Home] OK (<3s).");
        }
    }
}
