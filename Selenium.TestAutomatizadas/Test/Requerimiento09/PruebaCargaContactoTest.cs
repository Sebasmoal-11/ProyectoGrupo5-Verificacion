using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Test.Requerimiento09
{
    public class PruebaCargaContactoTest
    {
        private readonly string url;

        public PruebaCargaContactoTest(string url)
        {
            this.url = url;
        }

        public async Task Requerimiento09()
        {
            const int totalUsuarios = 50;
            
            var tareas = new List<Task>();
            var errores = new List<string>();

            for (int i = 1; i <= totalUsuarios; i++)
            {
                int usuarioId = i;

                var tarea = Task.Run(() => //lanza cada simulación en paralelo.
                {
                    try
                    {
                        SimularCarga(usuarioId);
                    }
                    catch (Exception ex)
                    {
                        lock (errores)
                        {
                            errores.Add($"Usuario {usuarioId}: {ex.Message}");
                        }
                    }

                    
                });

                tareas.Add(tarea);
            }

            await Task.WhenAll(tareas);
            // Resultado esperado
            Console.WriteLine("\n Resultado de la Prueba de Carga de la Página de Contacto:");
            if (errores.Count == 0)
            {
                Console.WriteLine("La página de Contacto se cargó correctamente para todos los usuarios.");
            }
            else
            {
                Console.WriteLine($"Se detectaron {errores.Count} errores durante la carga:");
                errores.ForEach(e => Console.WriteLine(e));
            }

        }

        private void SimularCarga(int usuarioId)
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(options))
            {
                    var stopwatch = Stopwatch.StartNew();
                    driver.Navigate().GoToUrl(url);

                // Espera que el enlace "Contacto" esté disponible
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.LinkText("Contacto")));

                //Hace clic en el enlace "Contacto"
                driver.FindElement(By.LinkText("Contacto")).Click();

                //Espera que se cargue la página de contacto
                wait.Until(d => d.Title.Contains("Contacto"));

                stopwatch.Stop();

                //Valida que el formulario esté presente
                try
                {
                    driver.FindElement(By.Id("contactForm"));
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("El formulario de contacto no se encontró en la página.");
                }

                Console.WriteLine($"Usuario {usuarioId}: Página de contacto cargada correctamente en {stopwatch.ElapsedMilliseconds} ms");

            }
        }
    }
}



