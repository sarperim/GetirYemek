using MassTransit;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infra
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Payment");
       
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();


            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount).HasPrecision(18, 2);

            });
        }

    }
}
