using CitiesManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Infrastructure.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<City>()
            .ToTable("Cities", schema: "dbo")
            .HasData(
                [
                    new City() { CityId = Guid.NewGuid(), Name = "New York" },
                    new City { CityId = Guid.NewGuid(), Name = "Los Angeles" },
                ]
            );
    }
}
