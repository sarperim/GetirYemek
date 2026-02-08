using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Povider { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
