using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.TestAutomatizadas.Pages;
using System;
using System.Threading;

namespace Selenium.TestAutomatizadas.Test.Requerimiento07
{
    public class ValidarDescripcionProductoTest
    {
        private readonly IWebDriver driver;
        private readonly ProductoPage productoPage;
        private readonly string url;

        public ValidarDescripcionProductoTest(string url)
        {
            driver = new ChromeDriver();
            this.url = url;
            productoPage = new ProductoPage(driver);
        }

        public void Requerimiento07()
        {
            try
            {
                // 1. Ir a la página index
                productoPage.IrAIndex(url);

                Thread.Sleep(2000);

                // 2. Hacer clic en "Agregar Producto"
                productoPage.ClickAgregarProducto();

                Thread.Sleep(2000); // Espera que cargue add-product.html

                // 3. Completar formulario con descripción larga
                string descripcionLarga = new string('A', 600);

                productoPage.CompletarFormulario("Producto Prueba", descripcionLarga, "1000");

                Thread.Sleep(1000);

                // 4. Enviar formulario
                productoPage.EnviarFormularioProducto();

                Thread.Sleep(3000);

                // Aquí puedes agregar validación del error si existe

            }
            finally
            {
                driver.Quit();
            }
        }

    }
}
