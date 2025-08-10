using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.TestAutomatizadas.Test.Requerimiento10
{
    public class PruebaCargaVariosProductosTest
    {
        private readonly string url;

        public PruebaCargaVariosProductosTest(string url)
        {
            this.url = url;
        }

        public async Task Requerimiento10()
        {
            const int totalUsuarios = 20;
            const int concurrenciaMaxima = 10;

            var semaphore = new SemaphoreSlim(concurrenciaMaxima);
            var tareas = new List<Task>();
            var errores = new List<string>();

            for (int i = 1; i <= totalUsuarios; i++)
            {
                int usuarioId = i;
                await semaphore.WaitAsync(); // Espera turno para ingresar las siguientes solicitudes

                var tarea = Task.Run(() =>
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
                    finally
                    {
                        semaphore.Release();
                    }
                });

                tareas.Add(tarea);
            }

            await Task.WhenAll(tareas);

            // Resultado esperado
            Console.WriteLine("\n Resultado de la Prueba de Carga del Sistema con Varios Productos:");
            if (errores.Count == 0)
            {
                Console.WriteLine("La página con Varios Productos se cargó correctamente para todos los usuarios.");
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

                // Espera que el enlace "Catalogo" esté disponible
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.LinkText("Catalogo")));

                //Hace clic en el enlace "Catalogo"
                driver.FindElement(By.LinkText("Catalogo")).Click();

                //Espera que se cargue la página de catalogo
                wait.Until(d => d.Title == "Catálogo de Productos");

                // 5️⃣ Esperar que al menos un producto esté presente
                wait.Until(d => d.FindElements(By.ClassName("product")).Count > 0);

                stopwatch.Stop();

                Console.WriteLine($"Usuario {usuarioId}: Catálogo cargado correctamente en {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}


