using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
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
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept; // auto-aceptar alerts

            using var driver = new ChromeDriver(options);

            var contacto = new ContactoPage(driver)
                .Ir(_baseUrl)
                .LlenarObligatorios(
                    nombre: "Usuario Prueba",
                    correo: $"qa{DateTime.UtcNow.Ticks}@mailinator.com",
                    mensaje: "Mensaje de prueba desde automatización")
                .EnviarFormulario();
            Console.WriteLine(driver.PageSource); // Para ver el HTML después de enviar


            if (!contacto.SeMostroExito())
                throw new Exception("[Contacto] No se detectó mensaje de éxito ni alert válido.");

            Console.WriteLine("[Contacto]  Envío correcto con mensaje de éxito.");
        }
    }
}
