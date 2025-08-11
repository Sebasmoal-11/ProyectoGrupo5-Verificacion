using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.ProductControllerTests //Req 8
{
    [TestClass]
    public class ProductDescriptionLengthUnitTest
    {
        [TestMethod]
        [TestCategory("Requerimiento - Longitud máxima de descripción en Product")]
        public void DescriptionMayorA500_EsNoValido()
        {
            // Arrange
            var descripcionLarga = new string('X', 501);
            var producto = new Product
            {
                Name = "Producto válido",
                Description = descripcionLarga,
                Price = 100m
            };
            var context = new ValidationContext(producto, null, null);
            var results = new List<ValidationResult>();

            // Act
            var esValido = Validator.TryValidateObject(producto, context, results, true);

            // Assert
            Assert.IsFalse(esValido, "No se debe permitir una descripción con más de 500 caracteres.");
            Assert.IsTrue(results.Count > 0, "Debe existir al menos un mensaje de error.");
        }
    }
}