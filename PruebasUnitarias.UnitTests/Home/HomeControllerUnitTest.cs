using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerNS = WebApplication1.Controllers;
using ProductModel = TiendaVirtualMVC.Models.Product;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        [TestMethod]
        public async Task IndexDevuelveListaProductos_ModelCorrecto()
        {
            var logger = new Mock<ILogger<ControllerNS.HomeController>>();
            var controller = new ControllerNS.HomeController(logger.Object); 

            var call = controller.Index();
            var result = call is Task<IActionResult> t ? await t : (IActionResult)call;

            var view = result as ViewResult;
            Assert.IsNotNull(view, "Index debe retornar un ViewResult.");
            Assert.IsNotNull(view!.Model, "Index debe pasar la lista a View(model).");

            Assert.IsInstanceOfType(view.Model!, typeof(IEnumerable<ProductModel>),
                $"El modelo debe ser IEnumerable<{typeof(ProductModel).FullName}>.");
        }
    }
}

