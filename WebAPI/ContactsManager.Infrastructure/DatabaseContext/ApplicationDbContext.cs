using CitiesManager.Core.Domain.Entities;
using ContactsManager.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Infrastructure.DatabaseContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
