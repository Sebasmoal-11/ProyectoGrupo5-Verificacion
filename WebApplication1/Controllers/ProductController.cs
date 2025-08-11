using Microsoft.AspNetCore.Mvc;
using TiendaVirtualMVC.Models;
using System.Collections.Generic;

namespace TiendaVirtualMVC.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Producto 1", Description = "Descripción 1", Price = 10 },
                new Product { Id = 2, Name = "Producto 2", Description = "Descripción 2", Price = 15 }
            };
        public IActionResult Index()
        {

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Asignar Id incremental
                product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                products.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }


        //Accion GET para mostrar el formulario de edición por Id
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Busca en la lista products el primer producto cuyo nombre coincida con el valor recibido como parámetro (nombre).Si no encuentra ninguno, devuelve null.
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //Accion POST para procesar la edicion de productos
        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                //Actualiza los productos
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;

                return RedirectToAction("Index");
            }

            return View(updatedProduct);
        }

    }
}

