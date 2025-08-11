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
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                ModelState.AddModelError(nameof(contact.Name), "El nombre es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(contact.Email))
            {
                ModelState.AddModelError(nameof(contact.Email), "El correo es obligatorio");
            }
            else if (!contact.Email.Contains("@") || !contact.Email.Contains("."))
            {
                ModelState.AddModelError(nameof(contact.Email), "El correo no tiene un formato válido");
            }

            if (string.IsNullOrWhiteSpace(contact.Message))
            {
                ModelState.AddModelError(nameof(contact.Message), "El mensaje es obligatorio");
            }

            if (!ModelState.IsValid)
            {
                // Retorna la vista con los errores para que se muestren con ValidationMessageFor
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

