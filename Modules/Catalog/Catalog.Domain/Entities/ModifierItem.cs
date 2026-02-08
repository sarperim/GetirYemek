using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class ModifierItem
    {
        public Guid Id { get; set; }
        public Guid ProductModifierId { get; set; }
        public string Name { get; set; }
        public decimal PriceAdjustment { get; set; }
        public bool IsActive { get; set; }
    }
}
