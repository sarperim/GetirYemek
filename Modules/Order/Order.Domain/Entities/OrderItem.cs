using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid FoodId { get; set; }
        public string FoodName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public List<OrderItemModifier> Modifiers { get; private set; }

    }
}
