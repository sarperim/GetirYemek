using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Entities
{
    public class OrderItemModifier
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public string ModifierName { get; set; }
        public decimal Price { get; set; }


    }
}
