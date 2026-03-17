using Catalog.Application.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


    namespace Catalog.Infra.Repositories
    {
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected readonly CatalogDbContext _dbContext;
            protected readonly DbSet<T> _dbSet;

            public GenericRepository(CatalogDbContext dbContext)
            {
                _dbContext = dbContext;
                _dbSet = _dbContext.Set<T>();
            }

            public void Add(T entity)
            {
                _dbSet.Add(entity);
            }

            public void Update(T entity)
            {
                _dbSet.Update(entity);
            }

            public void Remove(T entity)
            {
                _dbSet.Remove(entity);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task<T?> GetByIdAsync(Guid id)
            {
                return await _dbSet.FindAsync(id);
            }
        }
    }
