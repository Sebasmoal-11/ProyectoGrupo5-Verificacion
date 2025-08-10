using System;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;

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
            using var driver = new ChromeDriver(options);

            var home = new HomePage(driver);

            var usuarios = new[] { "alice", "bob", "charlie" }; // si no aplica usuarios, igual navega
            foreach (var u in usuarios)
            {
                var t = home.NavegarYMedirCarga(_baseUrl); // agrega ?user= si tu app lo usa
                Console.WriteLine($"[Carga Home] {u} -> {t.TotalMilliseconds:F0} ms");
                if (t.TotalSeconds > 3.0)
                    throw new Exception($"[Carga Home] Excedido: {t.TotalSeconds:F2}s (>3s) para {u}");
            }

            Console.WriteLine("[Carga Home] ✅ OK (<3s).");
        }
    }
}
