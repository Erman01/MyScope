using MyScope.Core.Models;
using MyShop.Core;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {
       IRepository<ProductCategory> productCategoryRepository;
        public ProductCategoryManagerController(IRepository<ProductCategory> _productCategoryRepository)
        {
            productCategoryRepository = _productCategoryRepository;
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = productCategoryRepository.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                productCategoryRepository.Insert(productCategory);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = productCategoryRepository.Find(id);
            if (productCategory==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory,string id)
        {
            ProductCategory productCategoryToEdit = productCategoryRepository.Find(id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                productCategoryToEdit.Name = productCategory.Name;
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategoryRepository.Find(id);
            if (productCategoryToDelete==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategoryToDelete = productCategoryRepository.Find(id);
            if (productCategoryToDelete==null)
            {
                return HttpNotFound();
            }
            else
            {
                productCategoryRepository.Delete(id);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}