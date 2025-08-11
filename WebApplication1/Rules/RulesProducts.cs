using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Rules
{
    public class RulesProduct
    {
        public static (bool, string) productoEsValido(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return (false, "El nombre es obligatorio");
            }

            else if (string.IsNullOrWhiteSpace(product.Description))
            {
                return (false, "La descripción es obligatoria");
            }

            else if (product.Price <= 0)
            {
                return (false, "El precio debe ser mayor a 0");
            }

            return (true, string.Empty);
        }
    }
}
