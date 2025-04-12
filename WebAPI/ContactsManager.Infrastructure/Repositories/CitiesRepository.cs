using CitiesManager.Core.Domain.Entities;
using CitiesManager.Infrastructure.DatabaseContext;
using ContactsManager.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Infrastructure.Repositories;

public class CitiesRepository(ApplicationDbContext db) : ICitiesRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<City> AddCityAsync(City city, CancellationToken cancellationToken = default)
    {
        _db.Cities.Add(city);
        bool rowsAffected = await _db.SaveChangesAsync(cancellationToken)
            .ContinueWith(task => task.Result > 0, cancellationToken);

        if (!rowsAffected)
        {
            throw new DbUpdateException("Failed to add the city to the database.");
        }

        return city;
    }

    public Task<bool> DeleteCityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _db.Cities.RemoveRange(_db.Cities.Where(c => c.CityId == id));
        return _db.SaveChangesAsync(cancellationToken)
            .ContinueWith(task => task.Result > 0, cancellationToken);
    }

    public Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
    {
        return _db
            .Cities.ToListAsync(cancellationToken)
            .ContinueWith(task => task.Result, cancellationToken);
    }

    public Task<List<City>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        return _db
            .Cities.Where(c => c.Name.Contains(name))
            .ToListAsync(cancellationToken)
            .ContinueWith(task => task.Result, cancellationToken);
    }

    public Task<City?> GetCityByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db
            .Cities.FirstOrDefaultAsync(c => c.CityId == id, cancellationToken)
            .ContinueWith(task => task.Result, cancellationToken);
    }

    public async Task<City> UpdateCityAsync(
        City city,
        CancellationToken cancellationToken = default
    )
    {
        var existingCity =
            await _db.Cities.FindAsync(
                [city.CityId, cancellationToken],
                cancellationToken: cancellationToken
            ) ?? throw new KeyNotFoundException($"City with ID {city.CityId} not found.");
        _db.Cities.Update(city);
        bool rowsAffected = await _db.SaveChangesAsync(cancellationToken)
            .ContinueWith(task => task.Result > 0, cancellationToken);

        if (!rowsAffected)
        {
            throw new DbUpdateException("Failed to update the city in the database.");
        }

        return city;
    }
}
