using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Responses.Catalog
{
    public class RestaurantResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal DeliveryRadius { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsActive { get; set; }
    }
}
