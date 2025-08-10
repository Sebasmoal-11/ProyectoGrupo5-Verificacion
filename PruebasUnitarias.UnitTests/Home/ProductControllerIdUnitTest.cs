
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using TiendaVirtualMVC.Controllers;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.ProductControllerTests
{
    [TestClass]
    public class ProductControllerIdUnitTest
    {
        /// <summary>
        /// Crea dos productos a través del controlador y verifica que el
        /// segundo producto obtiene un ID mayor que el primero. Ajusta
        /// la obtención del ID según la lógica real de tu controlador.
        /// </summary>
        [TestMethod]
        [TestCategory("Prueba ID único al crear producto")] // Requerimiento 1 
        public void CreateAsignarIdIncremental_GeneraIdUnico()
        {
            // Arrange: se crea el controlador y dos productos de prueba
            var controller = new ProductController();
            var producto1 = new Product
            {
                Name = "Producto 1",
                Description = "Primer producto",
                Price = 10m
            };
            var producto2 = new Product
            {
                Name = "Producto 2",
                Description = "Segundo producto",
                Price = 20m
            };

            // Act: se invoca el método Create dos veces
            controller.Create(producto1);
            controller.Create(producto2);

            // Assert: se verifica que los IDs estén asignados y que el
            // segundo ID sea mayor. Esta lógica presupone que el
            // controlador asigna IDs incrementales directamente sobre
            // los objetos producto.
            Assert.IsTrue(producto1.Id > 0, "El primer producto debería tener un ID asignado.");
            Assert.IsTrue(producto2.Id > 0, "El segundo producto debería tener un ID asignado.");
            Assert.IsTrue(producto2.Id > producto1.Id, "El ID del segundo producto debería ser mayor que el del primero.");
        }
    }
}