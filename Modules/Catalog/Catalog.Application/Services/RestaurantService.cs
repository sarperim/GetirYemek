using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Repository;
using Catalog.Domain.Entities;
using Contracts.Requests.Catalog;
using Contracts.Responses.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWorkCatalog _uow;

        public RestaurantService(IRestaurantRepository restaurantRepository, IUnitOfWorkCatalog uow)
        {
            _restaurantRepository = restaurantRepository;
            _uow = uow;
        }

        public async Task<Guid> CreateRestaurantAsync(Guid ownerId, RestaurantRequest request)
        {
            var newRestaurant = new Restaurant(
                ownerId,
                request.Name,
                request.Description,
                request.PhoneNumber,
                request.Address,
                request.City,
                request.Street,
                request.DeliveryRadius,
                request.Longitude,
                request.Latitude
            );

            _restaurantRepository.Add(newRestaurant);

            await _uow.SaveChangesAsync();

            return newRestaurant.Id;

        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task<RestaurantResponse?> GetRestaurantByIdAsync(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);

            if (restaurant == null)
            {
                return null;
            }

            return new RestaurantResponse
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                City = restaurant.City,
                Street = restaurant.Street,
                PhoneNumber = restaurant.PhoneNumber,
                DeliveryRadius = restaurant.DeliveryRadius,
                Longitude = restaurant.Longitude,
                Latitude = restaurant.Latitude,
                IsActive = restaurant.IsActive
            };
        }


    }
}
