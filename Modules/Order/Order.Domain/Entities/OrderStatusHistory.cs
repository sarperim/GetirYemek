using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Entities
{
    public class OrderStatusHistory
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
