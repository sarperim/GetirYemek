using Catalog.Application.Interfaces.Repository;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infra.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly CatalogDbContext _dbContext;
        public RestaurantRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Restaurant entity)
        {
            _dbContext.Restaurants.Add(entity);
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _dbContext.Restaurants
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Restaurants.FindAsync(id);
        }

        public void Remove(Restaurant entity)
        {
            _dbContext.Restaurants.Remove(entity);
        }

        public void Update(Restaurant entity)
        {
            _dbContext.Restaurants.Update(entity);
        }
    }
}
