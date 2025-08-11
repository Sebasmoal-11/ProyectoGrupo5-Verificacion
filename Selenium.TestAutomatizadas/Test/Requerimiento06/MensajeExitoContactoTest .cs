using System;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;
using OpenQA.Selenium;

namespace Selenium.TestAutomatizadas.Test.Requerimiento06 //Req 6
{
    public class MensajeExitoContactoTest
    {
        private readonly string _baseUrl;

        public MensajeExitoContactoTest(string baseUrl) => _baseUrl = baseUrl;

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
                    mensaje: "Prueba automatizada con éxito")
                .EnviarFormulario();

            if (!contacto.SeMostroExito())
                throw new Exception("[Contacto] ❌ No se mostró el mensaje de éxito tras enviar el formulario.");

            Console.WriteLine("[Contacto] ✅ Mensaje de éxito mostrado correctamente.");
        }
    }
}

