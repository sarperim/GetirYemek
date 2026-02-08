using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class ProductModifier
    {
        public Guid Id { get; set; }
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public int MinSelection { get; set; }
        public int MaxSelection { get; set; }
        
        public List<ModifierItem> ModifierItems { get; private set; } = new();
    }
}
