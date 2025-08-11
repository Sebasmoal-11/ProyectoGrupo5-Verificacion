using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Selenium.TestAutomatizadas.Pages;

namespace Selenium.TestAutomatizadas.Test.Requerimiento04   //requ 04
{
    public class ValidacionCamposVaciosContactoTest
    {
        private readonly string _baseUrl;

        public ValidacionCamposVaciosContactoTest(string baseUrl) => _baseUrl = baseUrl;

        public void Ejecutar()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;

            using var driver = new ChromeDriver(options);

            var contacto = new ContactoPage(driver)
                .Ir(_baseUrl);

            // Dejar todos los campos vacíos y enviar
            contacto.LlenarObligatorios("", "", "");
            contacto.EnviarFormulario();

            // Verificar mensajes de error
            bool nombreError = contacto.ExisteMensajeError("nombre");
            bool correoError = contacto.ExisteMensajeError("correo");
            bool mensajeError = contacto.ExisteMensajeError("mensaje");

            if (!nombreError || !correoError || !mensajeError)
                throw new Exception("[Contacto] ❌ No se mostraron mensajes de error en todos los campos obligatorios.");

            Console.WriteLine("[Contacto] ✅ Validación de campos vacíos correcta.");
        }
    }
}
