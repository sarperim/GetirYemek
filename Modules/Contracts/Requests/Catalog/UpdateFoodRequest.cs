using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Requests.Catalog
{
    public class UpdateFoodRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
