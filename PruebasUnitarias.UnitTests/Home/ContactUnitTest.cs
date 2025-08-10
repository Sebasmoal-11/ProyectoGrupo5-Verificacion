using TiendaVirtualMVC.Models;
using TiendaVirtualMVC.Rules;

namespace PruebasUnitarias.UnitTests.Home
{
    [TestClass]
    public class ContactUnitTest
    {
        private (bool, string) resultadoEsperado;
        private (bool, string) resultadoObtenido;
        private Contact contact;

        [TestMethod]
        [TestCategory("Validación de los campos del formulario de contacto")] //Requerimiento 9: se validan los campos esten presentes y no nulos o vacios.

        public void losCamposSonValidos_SonValidos()
        {
            contact = new Contact()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Message = "Prueba"
            };
            
            resultadoEsperado = (true, string.Empty);
            resultadoObtenido = RulesContact.formularioEsValido(contact);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [TestCategory("El nombre es nulo")] //Requerimiento 9: prueba cuando el nombre es nulo o vacio

        public void elNombreEsNulo_EsNoValido()
        {
            contact = new Contact()
            {
                Name = null,
                Email = "juan@gmail.com",
                Message = "Prueba"
            };

            resultadoEsperado = (false, "El nombre es obligatorio");
            resultadoObtenido = RulesContact.formularioEsValido(contact);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [TestCategory("El email es nulo")] //Requerimiento 9: prueba cuando el email es nulo o vacio

        public void elEmailEsNulo_EsNoValido()
        {
            contact = new Contact()
            {
                Name = "Juan",
                Email = null,
                Message = "Prueba"
            };

            resultadoEsperado = (false, "El email es obligatorio");
            resultadoObtenido = RulesContact.formularioEsValido(contact);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [TestCategory("El email no tiene formato valido")] //Requerimiento 9: prueba cuando el email no tiene formato valido

        public void elEmailFormatoNoValido_EsNoValido()
        {
            contact = new Contact()
            {
                Name = "Juan",
                Email = "juan#gmail,com",
                Message = "Prueba"
            };

            resultadoEsperado = (false, "El email no tiene un formato válido");
            resultadoObtenido = RulesContact.formularioEsValido(contact);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }

        [TestMethod]
        [TestCategory("El mensaje es nulo")] //Requerimiento 9: prueba cuando el mensaje es nulo o vacio

        public void elMensajeEsNulo_EsNoValido()
        {
            contact = new Contact()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Message = null
            };

            resultadoEsperado = (false, "El mensaje es obligatorio");
            resultadoObtenido = RulesContact.formularioEsValido(contact);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);
        }


    }


   
}



//Prueba 1234
