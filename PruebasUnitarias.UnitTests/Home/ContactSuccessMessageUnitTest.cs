using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApplication1.Controllers;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class ContactSuccessMessageUnitTest
    {
        [TestMethod]
        [TestCategory("Requerimiento - Mensaje de éxito en Contact")]
        public void MensajeExito_SeCargaEnTempData()
        {
            // Arrange
            ILogger<HomeController> logger = LoggerFactory.Create(builder => { }).CreateLogger<HomeController>();
            var controller = new HomeController(logger);

            var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
            );
            controller.TempData = tempData;

            // Act
            controller.TempData["SuccessMessage"] = "El mensaje fue enviado correctamente.";
            var resultado = controller.Contact();

            // Assert
            Assert.AreEqual("El mensaje fue enviado correctamente.", controller.TempData["SuccessMessage"]);
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));
        }
    }
}