using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryRadius { get; set; }
        bool IsActive { get; set; }
   
        public List<Food> Foods { get; private set; } = new();
        public List<RestaurantWorkingHour> RestaurantWorkingHours { get; private set; } = new();


    }
}
