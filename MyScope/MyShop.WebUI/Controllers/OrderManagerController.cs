using MyScope.Core.Contracts;
using MyScope.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;
        public OrderManagerController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }
        public ActionResult GetOrderById(string id)
        {
            Order order = orderService.GetOrder(id);
            return View(order);
        }
        public ActionResult UpdateOrder(string id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Completed"
            };

            Order orders = orderService.GetOrder(id);
            return View(orders);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order updateOrder,string id)
        {
            Order order = orderService.GetOrder(id);

            order.OrderStatus = updateOrder.OrderStatus;
            orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}