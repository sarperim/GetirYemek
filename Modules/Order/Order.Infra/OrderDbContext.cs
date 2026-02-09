using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Entities;
using Order.Domain;
using MassTransit;

namespace Order.Infra
{
    public class OrderDbContext : DbContext
    {
        public DbSet<SaleOrder> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemModifier> OrderItemModifiers { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Critical: Schema Isolation
            modelBuilder.HasDefaultSchema("Ordering");

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();


            // 2. Order Configuration
            modelBuilder.Entity<SaleOrder>(entity =>
            {
                // Money Precision (Required for SQL Server)
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);

                // Relationship: Order -> Items
                // "Order has many Items, linked by OrderId"
                entity.HasMany(o => o.Items)
                      .WithOne()            // Child (OrderItem) doesn't have an 'Order' property
                      .HasForeignKey(i => i.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: Order -> History
                entity.HasMany(o => o.History)
                      .WithOne()
                      .HasForeignKey(h => h.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 3. OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasPrecision(18, 2);

                // Relationship: OrderItem -> Modifiers
                entity.HasMany(i => i.Modifiers)
                      .WithOne()
                      .HasForeignKey(m => m.OrderItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 4. Modifier Configuration
            modelBuilder.Entity<OrderItemModifier>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });
        }
    
    }
}
