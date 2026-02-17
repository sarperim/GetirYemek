using Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<User?> GetByIdWithAddressAsync(Guid id);
    }
}
