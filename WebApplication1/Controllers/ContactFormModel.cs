using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualMVC.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; }
    }
}