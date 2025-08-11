using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiendaVirtualMVC.Models;
using TiendaVirtualMVC.Rules;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class ContactEmailValidationUnitTest
    {
        private (bool, string) esperado;
        private (bool, string) obtenido;
        private Contact contact;

        [TestMethod]
        [TestCategory("Requerimiento - Validación de formato de email en Contact")]
        public void EmailConFormatoCorrecto_EsValido()
        {
            // Arrange
            contact = new Contact()
            {
                Name = "Pedro",
                Email = "pedro@example.com",
                Message = "Consulta general"
            };
            esperado = (true, string.Empty);

            // Act
            obtenido = RulesContact.formularioEsValido(contact);

            // Assert
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        [TestCategory("Requerimiento - Validación de formato de email en Contact")]
        public void EmailConFormatoIncorrecto_EsNoValido()
        {
            // Arrange
            contact = new Contact()
            {
                Name = "Pedro",
                Email = "pedro#correo,com",
                Message = "Consulta general"
            };
            esperado = (false, "El email no tiene un formato válido");

            // Act
            obtenido = RulesContact.formularioEsValido(contact);

            // Assert
            Assert.AreEqual(esperado, obtenido);
        }
    }
}

