using TiendaVirtualMVC.Models;

namespace TiendaVirtualMVC.Rules
{
    public class RulesContact
    {
        public static (bool, string) formularioEsValido(Contact contact)
        {
            
            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                return (false, "El nombre es obligatorio");
            }

            else if (string.IsNullOrWhiteSpace(contact.Email))
            {
                return (false, "El email es obligatorio");
            }

            else if (!contact.Email.Contains("@") || !contact.Email.Contains("."))
            {
                return (false, "El email no tiene un formato válido");
            }

            else if (string.IsNullOrWhiteSpace(contact.Message))
            {
                return (false, "El mensaje es obligatorio");
            }

            return (true, string.Empty);
        }

    }
}
