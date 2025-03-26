using Entities.SeedData;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class ContactDbContext : DbContext
{
    public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Person>().ToTable("Persons", schema: "dbo");
        modelBuilder.Entity<Country>().ToTable("Countries", schema: "dbo");

        modelBuilder.Entity<Person>().HasData(PersonsMockData.GetPersons());
        modelBuilder.Entity<Country>().HasData(CountriesMockData.GetCountries());
    }

    public List<Person> GetPersonsStoredProcedure()
    {
        return [.. Persons.FromSqlRaw("EXEC [dbo].[GetPersons]")];
    }

    public int InsertPerson(Person person)
    {
        SqlParameter[] parameters =
        [
            new SqlParameter("@PersonId", person.PersonId),
            new SqlParameter("@FirstName", person.FirstName),
            new SqlParameter("@LastName", person.LastName),
            new SqlParameter("@Email", person.Email),
            new SqlParameter("@DateOfBirth", person.DateOfBirth),
            new SqlParameter("@Gender", person.Gender),
            new SqlParameter("@CountryId", person.CountryId),
            new SqlParameter("@Address", person.Address),
            new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters),
        ];

        return Database.ExecuteSqlRaw(
            "EXEC [dbo].[InsertPerson] @PersonId, @FirstName, @LastName, @Email, @DateOfBirth, @Gender, @CountryId, @Address, @ReceiveNewsLetters",
            parameters
        );
    }
}
