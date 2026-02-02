using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infra
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

            modelBuilder.Entity<User>()
                 .HasMany(u => u.Addresses)
                 .WithOne()
                 .HasForeignKey(a => a.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
