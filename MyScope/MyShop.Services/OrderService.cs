using MyScope.Core.Contracts;
using MyScope.Core.Models;
using MyScope.Core.ViewModels;
using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService:IOrderService
    {
        IRepository<Order> orderRepository;
        public OrderService(IRepository<Order> _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            orderRepository.Insert(baseOrder);
            orderRepository.Commit();
        }
        public List<Order> GetOrderList()
        {
            return orderRepository.Collection().ToList();
             
        }
        public Order GetOrder(string id)
        {
            return orderRepository.Find(id);
        }
        public void UpdateOrder(Order updatedOrder)
        {
            orderRepository.Update(updatedOrder);
            orderRepository.Commit();
           
        }
    }
}
