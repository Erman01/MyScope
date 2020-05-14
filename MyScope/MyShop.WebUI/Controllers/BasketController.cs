using MyScope.Core.Contracts;
using MyScope.Core.Models;
using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customerRespository;
        IBasketService basketService;
        IOrderService orderService;
        public BasketController(IBasketService _basketService,IOrderService _orderService, IRepository<Customer> _customerRespository)
        {
            customerRespository = _customerRespository;
            basketService = _basketService;
            orderService = _orderService;
        }
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string id)
        {
            basketService.AddToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string id)
        {
            basketService.RemoveFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = customerRespository.Collection().FirstOrDefault(c => c.EMail == User.Identity.Name);
            if (customer!=null)
            {
                Order order = new Order()
                {
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Street = customer.Street,
                    City = customer.City,
                    State = customer.State,
                    EMail = customer.EMail,
                    ZipCode = customer.ZipCode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }

            
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.EMail = User.Identity.Name;

            //Process Payment
            order.OrderStatus = "Payment Processed";

            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou",new {OrderId=order.Id });

        }
        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;

            return View();
        }
    }
}