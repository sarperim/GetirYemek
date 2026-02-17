using Auth.Application.Interfaces.Repository; // Verify this namespace matches your Interface
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        // --- Standard CRUD ---

        public void Add(User entity)
        {
            // Just adds to the ChangeTracker. 
            // The DB call happens when UnitOfWork.SaveChangesAsync() is called.
            _context.Users.Add(entity);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }

        public void Remove(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            // FindAsync is optimized to check the local cache first
            return await _context.Users.FindAsync(id);
        }

        // --- Auth Specific Logic ---

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking() // Optimization: Read-only query (faster)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            // AnyAsync is much faster than counting or fetching the user
            // We return TRUE if the email is unique (count is 0)
            return !await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithAddressAsync(Guid id)
        {
            return await _context.Users
                // FIX: Use the new property name 'Addresses'
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}