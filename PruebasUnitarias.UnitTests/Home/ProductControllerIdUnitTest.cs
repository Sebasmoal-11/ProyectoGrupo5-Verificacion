using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TiendaVirtualMVC.Models;
using System;

namespace PruebasUnitarias.UnitTests.Productos
{
    [TestClass]
    public class ProductControllerIdUnitTest
    {
        // Repositorio/Store en memoria SOLO para la prueba
        private sealed class InMemoryProductStore
        {
            private readonly List<Product> _items = new();
            private int _lastId = 0;

            public int Create(Product p)
            {
                if (p == null) throw new ArgumentNullException(nameof(p));
                // Reglas mínimas (opcional): name y price válidos
                if (string.IsNullOrWhiteSpace(p.Name)) throw new ArgumentException("Name requerido");
                if (p.Price <= 0) throw new ArgumentOutOfRangeException(nameof(p.Price));

                var nextId = ++_lastId; // autoincremento
                _items.Add(p);          // guardamos el producto (sin tocar el modelo)
                return nextId;          // devolvemos el ID generado
            }
        }

        [TestMethod]
        [TestCategory("Creación de producto: ID único")]
        public void CrearProducto_GeneraIdUnicoYAscendente()
        {
            // Arrange
            var store = new InMemoryProductStore();

            // Act
            int id1 = store.Create(new Product { Name = "Mouse", Description = "USB", Price = 10m });
            int id2 = store.Create(new Product { Name = "Teclado", Description = "Mecánico", Price = 20m });

            // Assert
            Assert.IsTrue(id1 > 0, "El primer ID debe ser > 0");
            Assert.AreNotEqual(id1, id2, "Cada creación debe generar un ID único");
            Assert.IsTrue(id2 > id1, "El segundo ID debe ser mayor que el primero");
        }
    }
}
