using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Requests.Basket
{
    public class AddToBasketRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
