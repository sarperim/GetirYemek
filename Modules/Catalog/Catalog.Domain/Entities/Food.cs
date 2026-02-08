using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class Food
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public List<ProductModifier> ProductModifiers { get; private set; } = new();

    }
}
