using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);

    }
}
