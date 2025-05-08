using ECommerceCartCrud.Core.Domain.Entities;
using ECommerceCartCrud.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCartCrud.Infrastructure.DBContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartVsProduct> CartVsProduct { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // tables
        modelBuilder
            .Entity<Product>()
            .ToTable("Products", schema: "dbo")
            .HasData(ProductSeedData.Seed());
        modelBuilder.Entity<Cart>().ToTable("Carts", schema: "dbo");
        modelBuilder.Entity<CartVsProduct>().ToTable("CartVsProducts", schema: "dbo");

        // table relations
        modelBuilder.Entity<CartVsProduct>().HasOne(cp => cp.Product);

        modelBuilder.Entity<Cart>().HasMany(c => c.CartProducts);
    }
}
