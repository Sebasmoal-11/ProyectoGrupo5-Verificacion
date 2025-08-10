using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Controllers;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class ContactViewUnitTest
    {
        [TestMethod]
        [TestCategory("Verificar que el método Contact devuelva la vista correcta")] //Requerimiento 10 

        public void ContactRetornaVistaCorrecta_EsCorrecta() 
        {
            //Se crea el logger manualmente porque hace falta el ILogger para simular el servicio del HomeController
            ILogger<HomeController> logger = LoggerFactory.Create(builder => { }).CreateLogger<HomeController>();

            //Arrange
            HomeController controlador = new HomeController(logger);

            //Act
            IActionResult resultado = controlador.Contact();

            //Assert
            Assert.IsInstanceOfType(resultado, typeof(ViewResult)); //Se verifica que el objeto resultado sea una instancia de la clase ViewResult

        }

    }

}

