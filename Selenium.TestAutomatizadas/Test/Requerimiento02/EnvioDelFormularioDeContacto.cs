using System;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;

namespace Selenium.TestAutomatizadas.Test.Requerimiento02
{
    public class EnvioDelFormularioDeContacto
    {
        private readonly string _baseUrl;
        public EnvioDelFormularioDeContacto(string baseUrl) => _baseUrl = baseUrl;

        public void Ejecutar()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            using var driver = new ChromeDriver(options);

            var contacto = new ContactoPage(driver)
                .Ir(_baseUrl)
                .LlenarObligatorios(
                    nombre: "Usuario Prueba",
                    correo: $"qa{DateTime.UtcNow.Ticks}@mailinator.com",
                    mensaje: "Mensaje de prueba desde automatización")
                .EnviarFormulario();

            if (!contacto.SeMostroExito())
                throw new Exception("[Contacto] No se mostró el mensaje de éxito.");

            Console.WriteLine("[Contacto] ✅ Envío correcto con mensaje de éxito.");
        }
    }
}
