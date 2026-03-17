using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Interfaces.Repository
{
    public interface IFoodRepository : IGenericRepository<Food>
    {
        void Add(Food entity);
        void Update(Food entity);
        void Remove(Food entity);
        Task<Food?> GetByIdAsync(Guid id);
        Task<IEnumerable<Food>> GetAllAsync();
        Task<Food?> GetByModifierIdAsync(Guid modifierId);
        Task<IEnumerable<Food>> GetByRestaurantIdAsync(Guid restaurantId);
    }
}
