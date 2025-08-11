using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TiendaVirtualMVC.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            var (esValido, mensajeError) = TiendaVirtualMVC.Rules.RulesContact.formularioEsValido(contact);

            if (!esValido)
            {
                ViewBag.Error = mensajeError;
                return View(contact);
            }

            ViewBag.Mensaje = "Su mensaje fue enviado correctamente.";
            return View();
        }

        public IActionResult Index()
        {
            var productos = new List<Product>
    {
        new Product { Name = "Producto 1", Description = "Descripción 1", Price = 10 },
        new Product { Name = "Producto 2", Description = "Descripción 2", Price = 15 }
        // puedes agregar más productos aquí
    };

            return View(productos);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

