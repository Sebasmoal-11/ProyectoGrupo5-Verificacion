
using System.ComponentModel.DataAnnotations;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class ProductValidation_ValidCases
    {
        [TestMethod]
        [TestCategory("Validación de campos del producto")]
        public void ProductoConCamposValidos_EsValido()
        {
            // Arrange
            var producto = new Product
            {
                Name = "Camisa",
                Description = "Camisa de algodón talla M",
                Price = 25.99m
            };

            var validationContext = new ValidationContext(producto, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(producto, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResults.Count);
        }
    }
}
