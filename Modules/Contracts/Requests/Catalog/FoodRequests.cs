using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Requests.Catalog
{
    public class CreateFoodRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class AddModifierRequest
    {
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public int MinSelection { get; set; }
        public int MaxSelection { get; set; }
    }

    public class AddModifierItemRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal PriceAdjustment { get; set; }
    }
}

