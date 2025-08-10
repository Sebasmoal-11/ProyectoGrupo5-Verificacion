using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.Audits;
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
            //string rutaWindows = @"file:///X:/Escritorio/ProyectoPaginaWeb/Pagina%20Web/index.html"; //aca ponen su ruta donde tienen guardada la pagina web  de Visual studio code
            //string rutaUrl = new Uri(rutaWindows).AbsoluteUri;
            string rutaUrl = "http://127.0.0.1:5500/Pagina%20Web/index.html"; //ejecutarlo desde el VS Code > Open with Live Server para las pruebas de carga automatizadas

            Console.WriteLine(rutaUrl);

            // Aquí agregan se agregan segun los requerimientos de esta manera

            //var nombre de la variable = new nombre de la clase de la prueba(rutaUrl);
            //Nombre de la variable.Nombre del requirimiento();

            // Ejecutar todas las pruebas 
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