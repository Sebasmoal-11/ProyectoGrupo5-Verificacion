using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.Productos
{
    [TestClass]
    public class ProductValidation_NegativePriceTests
    {
        [TestMethod]
        [TestCategory("Validación de precios negativos")]
        public void PrecioNegativo_EsNoValido_Model()
        {
            // Arrange
            var producto = new Product
            {
                Name = "Producto X",
                Description = "Alguna descripción",
                Price = -5m            // ← precio negativo
            };

            var ctx = new ValidationContext(producto);
            var results = new List<ValidationResult>();

            // Act
            var esValido = Validator.TryValidateObject(producto, ctx, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(esValido, "Un precio negativo debe invalidar el producto.");
            Assert.IsTrue(results.Count > 0, "Se esperaba al menos un error de validación.");

            // (Opcional) Confirma que el error corresponde al campo Price
            Assert.IsTrue(results.Any(r =>
                     r.MemberNames != null && r.MemberNames.Contains(nameof(Product.Price))),
                "Se esperaba un error asociado a la propiedad Price.");
        }
    }
}

