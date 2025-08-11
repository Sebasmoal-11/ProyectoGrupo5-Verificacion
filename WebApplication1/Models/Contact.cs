using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualMVC.Models
{
    public class Contact
    {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string Name { get; set; }

            [Required(ErrorMessage = "El correo es obligatorio")]
            [EmailAddress(ErrorMessage = "Formato de correo inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El mensaje es obligatorio")]
            public string Message { get; set; }
    }
}
