using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Responses.Catalog
{
    public class FoodResponse
    {
        public class FoodMenuResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public bool IsAvailable { get; set; }
            public List<ModifierResponse> Modifiers { get; set; } = new();
        }

        public class ModifierResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool IsRequired { get; set; }
            public int MinSelection { get; set; }
            public int MaxSelection { get; set; }
            public List<ModifierItemResponse> Items { get; set; } = new();
        }

        public class ModifierItemResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal PriceAdjustment { get; set; }
        }
    }
}
