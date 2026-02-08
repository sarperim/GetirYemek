using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infra
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<ProductModifier> ProductModifiers{ get; set; }
        public DbSet<ModifierItem> ModifierItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantWorkingHour> RestaurantWorkingHours { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.HasDefaultSchema("Catalog");

           modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurants");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Street).HasMaxLength(100);

            });

            modelBuilder.Entity<RestaurantWorkingHour>(entity =>
            {
                entity.ToTable("RestaurantWorkingHours");
                entity.HasKey(e => e.Id);

                // Relationship: One Restaurant -> Many WorkingHours
                entity.HasOne<Restaurant>()
                      .WithMany() // Assuming no navigation property in Restaurant class
                      .HasForeignKey(e => e.RestaurantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Foods");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Money precision

                // Relationship: One Restaurant -> Many Foods
                entity.HasOne<Restaurant>()
                      .WithMany()
                      .HasForeignKey(e => e.RestaurantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductModifier>(entity =>
            {
                entity.ToTable("ProductModifiers");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                // Relationship: One Food -> Many Modifiers
                entity.HasOne<Food>()
                      .WithMany()
                      .HasForeignKey(e => e.FoodId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ModifierItem>(entity =>
            {
                entity.ToTable("ModifierItems");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PriceAdjustment).HasColumnType("decimal(18,2)");

                entity.HasOne<ProductModifier>()
                      .WithMany()
                      .HasForeignKey(e => e.ProductModifierId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }

}
