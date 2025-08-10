using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TiendaVirtualMVC.Controllers;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.ProductControllerTests
{
    [TestClass]
    public class ProductControllerUnitTest
    {
        [TestMethod]
        public void Create_ProductoValido_RedireccionaIndex()
        {
            // Arrange
            var controller = new ProductController();

            var nuevoProducto = new Product
            {
                Name = "Zapatos",
                Description = "Zapatos deportivos talla 42",
                Price = 59.99m
            };

            // Act
            var resultado = controller.Create(nuevoProducto);

            // Assert
            // Verificamos que el resultado es una redirección a la acción Index
            Assert.IsInstanceOfType(resultado, typeof(RedirectToActionResult));

            var redirectResult = resultado as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}


