using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain
{
    public class SaleOrder
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        DateTime CreatedAt { get; set; }
        public string DeliveryAddress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public List<OrderItem> Items { get; private set; } = new();
        public List<OrderStatusHistory> History { get; private set; } = new();


    }
}
