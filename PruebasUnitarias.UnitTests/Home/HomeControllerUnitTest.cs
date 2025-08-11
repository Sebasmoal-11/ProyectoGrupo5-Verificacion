using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TiendaVirtualMVC.Controllers;
using WebApplication1.Controllers;
//Ajusta el namespace del modelo si es diferente
using ProductModel = TiendaVirtualMVC.Models.Product;

namespace PruebasUnitarias.UnitTests.Home
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetAll();
    }

    [TestClass]
    public class HomeControllerUnitTest
    {
        [TestMethod]
        [TestCategory("Funcionalidad Index en HomeController")]
        public async Task IndexDevuelveListaProductos_ModelCorrecto()
        {
            // Arrange: mock del repo retornando una lista
            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.GetAll()).Returns(new List<ProductModel>
            {
                new() { Name = "Mouse", Description = "USB", Price = 10m }
            });

            var controller = new HomeController(repo.Object); // <-- ctor: HomeController(IProductRepository repo)

            // Act (sync/async)
            IActionResult result;
            var call = controller.Index();
            result = call is Task<IActionResult> t ? await t : (IActionResult)call;

            // Assert
            var view = result as ViewResult;
            Assert.IsNotNull(view, "Index debe retornar un ViewResult.");
            Assert.IsInstanceOfType(view!.Model, typeof(IEnumerable<ProductModel>));

            var lista = (IEnumerable<ProductModel>)view.Model!;
            Assert.IsTrue(lista.Any(), "Se esperaba al menos un producto en la lista.");
        }
    }
}
