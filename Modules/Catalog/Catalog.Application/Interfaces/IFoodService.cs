using Contracts.Requests.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using static Contracts.Responses.Catalog.FoodResponse;

namespace Catalog.Application.Interfaces
{
    public interface IFoodService
    {
        Task<Guid> CreateFoodAsync(Guid ownerId, Guid restaurantId, CreateFoodRequest request);
        Task<Guid> AddModifierAsync(Guid ownerId, Guid foodId, AddModifierRequest request);
        Task<Guid> AddModifierItemAsync(Guid ownerId, Guid modifierId, AddModifierItemRequest request);
        Task<IEnumerable<FoodMenuResponse>> GetRestaurantMenuAsync(Guid id);
        Task DeleteFoodAsync(Guid ownerId, Guid foodId);
        Task UpdateFoodAsync(Guid ownerId, Guid foodId, UpdateFoodRequest request);
    }
}
