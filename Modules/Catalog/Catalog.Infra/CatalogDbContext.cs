using Catalog.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<ProductModifier> ProductModifiers { get; set; }
        public DbSet<ModifierItem> ModifierItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantWorkingHour> RestaurantWorkingHours { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Catalog");

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

            // 1. Restaurant Configuration
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurants");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OwnerId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.DeliveryRadius).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<RestaurantWorkingHour>(entity =>
            {
                entity.ToTable("RestaurantWorkingHours");
                entity.HasKey(e => e.Id);

                entity.HasOne<Restaurant>()
                      .WithMany(r => r.RestaurantWorkingHours) 
                      .HasForeignKey(e => e.RestaurantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 3. Food Configuration
            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Foods");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.HasOne<Restaurant>()
                      .WithMany(r => r.Foods) 
                      .HasForeignKey(e => e.RestaurantId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 4. ProductModifier Configuration
            modelBuilder.Entity<ProductModifier>(entity =>
            {
                entity.ToTable("ProductModifiers");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasOne<Food>()
                      .WithMany(f => f.ProductModifiers) // <-- Explicitly mapped!
                      .HasForeignKey(e => e.FoodId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 5. ModifierItem Configuration
            modelBuilder.Entity<ModifierItem>(entity =>
            {
                entity.ToTable("ModifierItems");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PriceAdjustment).HasColumnType("decimal(18,2)");

                entity.HasOne<ProductModifier>()
                      .WithMany(p => p.ModifierItems)
                      .HasForeignKey(e => e.ProductModifierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

}
