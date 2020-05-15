using MyScope.Core.Models;
using MyScope.Core.ViewModels;
using MyShop.Core;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> productRepository;
        IRepository<ProductCategory> productCategoryRepository;
        public ProductManagerController(IRepository<Product> _productRepository, IRepository<ProductCategory> _productCategoryRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = productRepository.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {

            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoryRepository.Collection();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                productRepository.Insert(product);
                productRepository.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string id)
        {
            Product product = productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                viewModel.Product = product;
                viewModel.ProductCategories = productCategoryRepository.Collection();

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = productRepository.Find(id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Price = product.Price;
                productToEdit.Description = product.Description;

                productRepository.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string id)
        {
            Product product = productRepository.Find(id);
            if (id == null)
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
            if (productToDelete == null)
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