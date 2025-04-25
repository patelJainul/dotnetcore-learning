using ECommerceCart.Core.Domain.Entities;
using ECommerceCart.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCart.Infrastructure.DbContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartVsProducts> CartVsProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Product>()
            .ToTable("Products", schema: "dbo")
            .HasData(
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Sample Product",
                    Description = "This is a sample product.",
                    Price = 10.99m,
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Another Product",
                    Description = "This is another sample product.",
                    Price = 15.99m,
                }
            );

        modelBuilder.Entity<Cart>().ToTable("Carts", schema: "dbo");

        modelBuilder.Entity<CartVsProducts>().ToTable("CartVsProducts", schema: "dbo");

        modelBuilder.Entity<Order>().ToTable("Orders", schema: "dbo");
        modelBuilder.Entity<OrderVsProduct>().ToTable("OrderVsProducts", schema: "dbo");
        modelBuilder.Entity<Address>().ToTable("Addresses", schema: "dbo");

        //Table Relations
        modelBuilder.Entity<Cart>().HasMany(c => c.CartVsProducts);
        modelBuilder.Entity<Order>().HasMany(o => o.OrderVsProducts);
        modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Addresses);
    }
}
