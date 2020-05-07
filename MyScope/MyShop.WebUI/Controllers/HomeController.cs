using MyScope.Core.Models;
using MyScope.Core.ViewModels;
using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productRepository;
        IRepository<ProductCategory> productCategoryRepository;
        public HomeController(IRepository<Product> _productRepository, IRepository<ProductCategory> _productCategoryRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
        }
        public ActionResult Index(string categoryList=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = productCategoryRepository.Collection().ToList();

            if (categoryList == null)
            {
                products = productRepository.Collection().ToList();
            }
            else
            {
                products = productRepository.Collection().Where(p => p.Category == categoryList).ToList();
            }
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = products,
                ProductCategories = productCategories
            };

            return View(model);
        }
        public ActionResult Details(string id)
        {
            Product product = productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}