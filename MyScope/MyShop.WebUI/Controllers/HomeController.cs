using MyScope.Core.Models;
using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productRepository;
        IRepository<ProductCategory> productCategoryRepository;
        public HomeController(IRepository<Product> _productRepository,IRepository<ProductCategory> _productCategoryRepository)
        {
            productRepository = _productRepository;
            productCategoryRepository = _productCategoryRepository;
        }
        public ActionResult Index()
        {
            List<Product> products = productRepository.Collection().ToList();
            return View(products);
        }
        public ActionResult Details(string id)
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