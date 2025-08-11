
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TiendaVirtualMVC.Controllers;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        /// <summary>
        /// Esta prueba verifica que la acción Index del HomeController
        /// devuelve una vista con un modelo de tipo lista de productos.
        /// </summary>
        [TestMethod]
        [TestCategory("Funcionalidad Index en HomeController")] // Requerimiento 5 
        public void IndexDevuelveListaProductos_NoEsNull()
        {
            // Arrange: se crea una instancia del controlador
            var controller = new WebApplication1.Controllers.HomeController();

            // Act: se invoca el método Index
            var resultado = controller.Index();

            // Assert: se comprueba que el resultado es una ViewResult
            // y que su modelo es una lista de productos
            var vista = resultado as ViewResult;
            Assert.IsNotNull(vista, "Index debería retornar un objeto ViewResult");
            var modelo = vista?.Model as List<Product>;
            Assert.IsNotNull(modelo, "El modelo devuelto por Index debería ser una lista de productos.");
        }
    }
}