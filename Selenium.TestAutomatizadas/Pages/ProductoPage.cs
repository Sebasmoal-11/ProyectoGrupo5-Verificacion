using OpenQA.Selenium;

namespace Selenium.TestAutomatizadas.Pages
{
    public class ProductoPage
    {
        private readonly IWebDriver driver;

        public ProductoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Localizadores para formulario (solo en add-product.html)
        public IWebElement InputNombre => driver.FindElement(By.Id("productName"));
        public IWebElement InputDescripcion => driver.FindElement(By.Id("productDescription"));
        public IWebElement InputPrecio => driver.FindElement(By.Id("productPrice"));
        public IWebElement BotonAgregar => driver.FindElement(By.CssSelector("button[type='submit']"));

        // Localizador para el enlace "Agregar Producto" en index.html
        public IWebElement LinkAgregarProducto => driver.FindElement(By.LinkText("Agregar Producto"));

        // Método para ir a la página principal (index.html)
        public void IrAIndex(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        // Método para hacer clic en el enlace Agregar Producto
        public void ClickAgregarProducto()
        {
            LinkAgregarProducto.Click();
        }

        // Métodos para completar formulario
        public void EscribirNombre(string nombre)
        {
            InputNombre.Clear();
            InputNombre.SendKeys(nombre);
        }

        public void EscribirDescripcion(string descripcion)
        {
            InputDescripcion.Clear();
            InputDescripcion.SendKeys(descripcion);
        }

        public void EscribirPrecio(string precio)
        {
            InputPrecio.Clear();
            InputPrecio.SendKeys(precio);
        }

        public void CompletarFormulario(string nombre, string descripcion, string precio)
        {
            EscribirNombre(nombre);
            EscribirDescripcion(descripcion);
            EscribirPrecio(precio);
        }

        public void EnviarFormularioProducto()
        {
            BotonAgregar.Click();
        }
    }
}
