using Catalog.Application.Interfaces.Repository;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infra.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CatalogDbContext _context;
        public FoodRepository(CatalogDbContext context)
        {
            _context = context;
        }
        public void Add(Food entity)
        {
            _context.Foods.Add(entity);
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            return await _context.Foods
                .Include(f => f.ProductModifiers)
                    .ThenInclude(pm => pm.ModifierItems)
                .ToListAsync();
        }

        public async Task<Food?> GetByIdAsync(Guid id)
        {
            return await _context.Foods
                .Include(f => f.ProductModifiers)
                    .ThenInclude(pm => pm.ModifierItems)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Food?> GetByModifierIdAsync(Guid modifierId)
        {
            return await _context.Foods
            .Include(f => f.ProductModifiers)
            .ThenInclude(pm => pm.ModifierItems)
            .FirstOrDefaultAsync(f => f.ProductModifiers.Any(pm => pm.Id == modifierId));
        }

        public void Remove(Food entity)
        {
            _context.Foods.Remove(entity);
        }

        public void Update(Food entity)
        {
            _context.Foods.Update(entity);
        }
        public async Task<IEnumerable<Food>> GetByRestaurantIdAsync(Guid restaurantId)
        {
            return await _context.Foods
                .Include(f => f.ProductModifiers)
                    .ThenInclude(pm => pm.ModifierItems)
                .Where(f => f.RestaurantId == restaurantId && f.IsAvailable) 
                .ToListAsync();
        }
    }
}
