using Microsoft.AspNetCore.Mvc;

namespace TiendaVirtualMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult ContactSuccess()
        {
            TempData["SuccessMessage"] = "Mensaje enviado con éxito";
            return View();
        }
    }
}
