using System.ComponentModel.DataAnnotations;

namespace TiendaVirtualMVC.Models
{
    public class Product
    {
        public int Id { get; set; } //agregado para la prueba de carga 9

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }
    }
}
