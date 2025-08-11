using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.Audits;
using Selenium.TestAutomatizadas.Test;
using Selenium.TestAutomatizadas.Test.Requerimiento01;
using Selenium.TestAutomatizadas.Test.Requerimiento02;
using Selenium.TestAutomatizadas.Test.Requerimiento03;
using Selenium.TestAutomatizadas.Test.Requerimiento07;
using Selenium.TestAutomatizadas.Test.Requerimiento08;
using Selenium.TestAutomatizadas.Test.Requerimiento09;
using Selenium.TestAutomatizadas.Test.Requerimiento10;

namespace Selenium.TestAutomatizadas
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string rutaUrl = "https://localhost:7202"; //ejecutarlo desde el VS Code > Open with Live Server para las pruebas de carga automatizadas

            Console.WriteLine(rutaUrl);

            // === NUEVAS AUTOMATIZADAS (nombres según la especificación) ===
            var cargaHome = new CargaDeLaPaginaDeInicio(rutaUrl);
            cargaHome.Ejecutar();

            var envioContacto = new EnvioDelFormularioDeContacto(rutaUrl);
            envioContacto.Ejecutar();

            var creacionProductos = new CreacionDeProductos(rutaUrl);
            creacionProductos.Ejecutar();


            var pruebaDescripcionProducto = new ValidarDescripcionProductoTest(rutaUrl);
            pruebaDescripcionProducto.Requerimiento07();

            var pruebaDatosProducto = new ValidarDatosInvalidosProductoTest(rutaUrl);
            pruebaDatosProducto.Requerimiento08();

            var pruebaCargaContacto = new PruebaCargaContactoTest(rutaUrl);
            await pruebaCargaContacto.Requerimiento09(); //await para evitar que una tarea termine sin bloquear el hilo.

            var pruebaCargaVariosProductos = new PruebaCargaVariosProductosTest(rutaUrl);
            await pruebaCargaVariosProductos.Requerimiento10();
        }
    }
}