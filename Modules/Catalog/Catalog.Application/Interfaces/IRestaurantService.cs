using Catalog.Domain.Entities;
using Contracts.Requests.Catalog;
using Contracts.Responses.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<Guid> CreateRestaurantAsync(Guid ownerId, RestaurantRequest request);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<RestaurantResponse?> GetRestaurantByIdAsync(Guid id);
    }
}
