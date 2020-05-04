using MyScope.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository productRepository;
        public ProductManagerController()
        {
            productRepository = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = productRepository.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                productRepository.Insert(product);
                productRepository.Commit();

                return RedirectToAction("Index");
            }
          
        }
        public ActionResult Edit(string id)
        {
            Product product = productRepository.Find(id);
            if (product==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
          
        }
        [HttpPost]
        public ActionResult Edit(Product product,string id)
        {
            Product productToEdit = productRepository.Find(id);
            if (productToEdit==null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {

                }
                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;
                productToEdit.Description = product.Description;

                productRepository.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string id)
        {
            Product product = productRepository.Find(id);
            if (id==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = productRepository.Find(id);
            if (productToDelete==null)
            {
                return HttpNotFound();
            }
            else
            {
                productRepository.Delete(id);
                productRepository.Commit();
                return RedirectToAction("Index");

            }
        }
       
    }
}