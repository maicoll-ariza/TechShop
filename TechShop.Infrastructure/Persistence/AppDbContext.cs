using Microsoft.EntityFrameworkCore;
using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.ImageUrl)
                .HasMaxLength(500);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Total)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.Property(ci => ci.Quantity)
                .IsRequired();
        });

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Laptops", Description = "Computadores portátiles", IsActive = true },
            new Category { Id = 2, Name = "Smartphones", Description = "Teléfonos inteligentes", IsActive = true },
            new Category { Id = 3, Name = "Accesorios", Description = "Accesorios tecnológicos", IsActive = true }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "MacBook Pro M3", Description = "Laptop Apple con chip M3", Price = 1999.99m, Stock = 10, ImageUrl = "macbook.jpg", IsActive = true, CategoryId = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "iPhone 15 Pro", Description = "Smartphone Apple", Price = 999.99m, Stock = 25, ImageUrl = "iphone.jpg", IsActive = true, CategoryId = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Samsung Galaxy S24", Description = "Smartphone Samsung", Price = 899.99m, Stock = 20, ImageUrl = "samsung.jpg", IsActive = true, CategoryId = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Dell XPS 15", Description = "Laptop Dell premium", Price = 1499.99m, Stock = 8, ImageUrl = "dell.jpg", IsActive = true, CategoryId = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "AirPods Pro", Description = "Audífonos inalámbricos Apple", Price = 249.99m, Stock = 50, ImageUrl = "airpods.jpg", IsActive = true, CategoryId = 3, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}