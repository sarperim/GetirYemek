namespace Catalog.Domain.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public decimal DeliveryRadius { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsActive { get; set; }


        private readonly List<Food> _foods = new();
        public IReadOnlyCollection<Food> Foods => _foods.AsReadOnly();

        private readonly List<RestaurantWorkingHour> _restaurantWorkingHours = new();
        public IReadOnlyCollection<RestaurantWorkingHour> RestaurantWorkingHours => _restaurantWorkingHours.AsReadOnly();

        private Restaurant() { }
        public Restaurant(Guid ownerId, string name, string description, string phoneNumber)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;
            Name = name;
            Description = description;
            PhoneNumber = phoneNumber;
        }

        public Restaurant(Guid ownerId, string name, string description, string phoneNumber, string address, string city, string street, decimal deliveryRadius, double longitude, double latitude) : this(ownerId, name, description, phoneNumber)
        {
            Address = address;
            City = city;
            Street = street;
            DeliveryRadius = deliveryRadius;
            Longitude = longitude;
            Latitude = latitude;
            IsActive = true;

        }

        public void AddFood(Food food)
        {
            _foods.Add(food);
        }

        public void AddRestaurantWorkingHours(RestaurantWorkingHour restaurantWorkingHour)
        {
            _restaurantWorkingHours.Add(restaurantWorkingHour);
        }
    }
}
