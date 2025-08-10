using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium.TestAutomatizadas.Test.Requerimiento08
{
    internal class ValidarDatosInvalidosProductoTest
    {
        private readonly IWebDriver driver;
        private readonly ProductoPage productoPage;
        private readonly string url;

        public ValidarDatosInvalidosProductoTest(string url)
        {
            driver = new ChromeDriver();
            this.url = url;
            productoPage = new ProductoPage(driver);
        }

        public void Requerimiento08()
        {
            try
            {
                // Abrir página principal
                productoPage.IrAIndex(url);

                // Caso 1: Nombre vacío
                productoPage.ClickAgregarProducto();
                Thread.Sleep(1000); // espera simple
                productoPage.EscribirNombre("");
                productoPage.EscribirDescripcion("Descripción válida");
                productoPage.EscribirPrecio("500");
                productoPage.EnviarFormularioProducto();

                Thread.Sleep(2000);
                if (driver.Url.Contains("add-product.html"))
                    Console.WriteLine("Nombre vacío: correctamente rechazado");
                else
                    Console.WriteLine("Nombre vacío: ERROR, se aceptó el dato inválido");

                // Volver a index
                productoPage.IrAIndex(url);

                // Caso 2: Precio negativo
                productoPage.ClickAgregarProducto();
                Thread.Sleep(1000);
                productoPage.EscribirNombre("Producto Test");
                productoPage.EscribirDescripcion("Descripción válida");
                productoPage.EscribirPrecio("-1500");
                productoPage.EnviarFormularioProducto();

                Thread.Sleep(2000);
                if (driver.Url.Contains("add-product.html"))
                    Console.WriteLine("Precio negativo: correctamente rechazado");
                else
                    Console.WriteLine("Precio negativo: ERROR, se aceptó el dato inválido");

                productoPage.IrAIndex(url);

                // Caso 3: Descripción vacía
                productoPage.ClickAgregarProducto();
                Thread.Sleep(1000);
                productoPage.EscribirNombre("Producto Test");
                productoPage.EscribirDescripcion("");
                productoPage.EscribirPrecio("1500");
                productoPage.EnviarFormularioProducto();

                Thread.Sleep(2000);
                if (driver.Url.Contains("add-product.html"))
                    Console.WriteLine("Descripción vacía: correctamente rechazado");
                else
                    Console.WriteLine("Descripción vacía: ERROR, se aceptó el dato inválido");

                Console.WriteLine("Pruebas con datos inválidos finalizadas.");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
