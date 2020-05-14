﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScope.Core.Models
{
    public class Order:BaseEntity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


    }
}
