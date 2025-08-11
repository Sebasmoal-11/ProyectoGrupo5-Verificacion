using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TiendaVirtualMVC.Controllers;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.ProductControllerTests
{
    [TestClass]
    public class ProductController_Create_ValidProduct_Test
    {
        [TestMethod]
        [TestCategory("Requerimiento 6 - Crear producto con datos válidos")] //Requerimiento 6
        public void Create_ProductoValido_AgregaAListaYRedirige()
        {
            // Arrange
            var controller = new ProductController();
            var nuevoProducto = new TiendaVirtualMVC.Models.Product

            {
                Id = 3,
                Name = "Camisa",
                Description = "Camisa de algodón talla M",
                Price = 25.99m
            };

            // Act
            var resultado = controller.Create(nuevoProducto);

            // Assert - verifica que redirige al Index
            Assert.IsInstanceOfType(resultado, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)resultado;
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Para verificar que el producto se agregó, revisamos si está en la lista estática
            var productosField = typeof(ProductController)
                .GetField("products", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var productos = (System.Collections.Generic.List<TiendaVirtualMVC.Models.Product>)productosField.GetValue(null);

            Assert.IsTrue(productos.Any(p => p.Name == "Camisa" && p.Description == "Camisa de algodón talla M"));
        }
    }
}