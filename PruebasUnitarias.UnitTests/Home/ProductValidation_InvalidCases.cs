
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TiendaVirtualMVC.Models;

namespace PruebasUnitarias.UnitTests.Home
{
    /// <summary>
    /// Conjunto de pruebas unitarias que verifican el comportamiento de la entidad
    /// Product ante datos inválidos. Cada prueba se centra en una regla de
    /// validación específica: precio negativo, nombre vacío y descripción demasiado larga.
    /// </summary>
    [TestClass]
    public class ProductValidation_InvalidCases
    {
        /// <summary>
        /// Verifica que el validador de DataAnnotations marque como inválido un
        /// producto cuyo precio es negativo. De acuerdo con los requisitos, los
        /// precios negativos deben rechazarse.
        /// </summary>
        [TestMethod]
        [TestCategory("Validación de precios negativos")]//Requerimiento 2
        public void PrecioNegativo_EsNoValido()
        {
            // Arrange: se crea un producto con un precio negativo
            var producto = new Product
            {
                Name = "Producto de prueba",
                Description = "Descripción corta",
                Price = -10m
            };
            var context = new ValidationContext(producto, null, null);
            var results = new List<ValidationResult>();

            // Act: se valida el objeto
            var esValido = Validator.TryValidateObject(producto, context, results, true);

            // Assert: la validación debe fallar y debe existir al menos un error
            Assert.IsFalse(esValido, "Un producto con precio negativo debería ser inválido.");
            Assert.IsTrue(results.Count > 0, "Se esperaba al menos un mensaje de error al validar un precio negativo.");
        }

        /// <summary>
        /// Verifica que la validación de DataAnnotations rechace un producto con
        /// nombre vacío o nulo. La lógica de la aplicación no debería permitir
        /// nombres vacíos.
        /// </summary>
        [TestMethod]
        [TestCategory("Validación de nombre vacío")]
        public void NombreVacio_EsNoValido()
        {
            // Arrange: se crea un producto sin nombre
            var producto = new Product
            {
                Name = string.Empty,
                Description = "Tiene descripción",
                Price = 15m
            };
            var context = new ValidationContext(producto, null, null);
            var results = new List<ValidationResult>();

            // Act
            var esValido = Validator.TryValidateObject(producto, context, results, true);

            // Assert
            Assert.IsFalse(esValido, "No debería permitirse crear un producto sin nombre.");
            Assert.IsTrue(results.Count > 0, "Se esperaba al menos un mensaje de error para el nombre vacío.");
        }

        /// <summary>
        /// Comprueba que un producto cuya descripción supera los 500 caracteres
        /// sea considerado inválido por las reglas de validación. Esto aplica
        /// únicamente si se define un atributo de longitud máxima en el modelo.
        /// </summary>
        [TestMethod]
        [TestCategory("Validación de longitud de descripción")]
        public void DescripcionMayor500_EsNoValido()
        {
            // Arrange: se crea una descripción de más de 500 caracteres
            var descripcionLarga = new string('A', 501);
            var producto = new Product
            {
                Name = "Nombre válido",
                Description = descripcionLarga,
                Price = 20m
            };
            var context = new ValidationContext(producto, null, null);
            var results = new List<ValidationResult>();

            // Act
            var esValido = Validator.TryValidateObject(producto, context, results, true);

            // Assert
            Assert.IsFalse(esValido, "La descripción que supera los 500 caracteres debe ser inválida.");
            Assert.IsTrue(results.Count > 0, "Se esperaba un mensaje de error por longitud excesiva de la descripción.");
        }
    }
}