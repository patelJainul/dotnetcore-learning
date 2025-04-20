using CitiesManager.Core.Domain.Entities;
using CitiesManager.Infrastructure.DatabaseContext;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Infrastructure.Repositories;

public class CitiesRepository(ApplicationDbContext db) : ICityRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<City?> CityExistsAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        // check if the city exists in the database
        return await _db.Cities.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<City> AddCityAsync(City city, CancellationToken cancellationToken = default)
    {
        var isExist = await CityExistsAsync(city.Name, cancellationToken);

        if (isExist != null)
        {
            throw new DbUpdateException("City already exists.");
        }
        // add city to the database
        _db.Cities.Add(city);

        // check that the city was added successfully
        bool rowsAffected = (await _db.SaveChangesAsync(cancellationToken)) > 0;

        // if the city was not added successfully, throw an exception
        if (!rowsAffected)
        {
            throw new DbUpdateException("Failed to add the city to the database.");
        }

        return city;
    }

    public async Task<JsonResponse<bool>> DeleteCityAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        // remove the city from the database
        _db.Cities.RemoveRange(_db.Cities.Where(c => c.CityId == id));
        var isDeleted = (await _db.SaveChangesAsync(cancellationToken)) > 0;
        // save changes to the database
        return new JsonResponse<bool>
        {
            Data = isDeleted,
            Message = isDeleted
                ? "City deleted successfully."
                : "Failed to delete the city or already deleted.",
        };
    }

    public Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
    {
        // get all cities from the database
        return _db.Cities.ToListAsync(cancellationToken);
    }

    public Task<List<City>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        // get cities by name from the database
        return _db.Cities.Where(c => c.Name.Contains(name)).ToListAsync(cancellationToken);
    }

    public Task<City?> GetCityByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // get city by id from the database
        return _db.Cities.FirstOrDefaultAsync(c => c.CityId == id, cancellationToken);
    }

    public async Task<City?> UpdateCityAsync(
        City city,
        CancellationToken cancellationToken = default
    )
    {
        // check that the city exists in the database
        var existingCity =
            await _db
                .Cities.AsNoTracking()
                .FirstOrDefaultAsync(
                    c => c.CityId == city.CityId,
                    cancellationToken: cancellationToken
                ) ?? throw new KeyNotFoundException($"City with ID {city.CityId} not found.");

        // update the city in the database and check that the city was updated successfully
        _db.Cities.Update(city);
        int rowsAffected = await _db.SaveChangesAsync(cancellationToken);

        // if the city was not updated successfully, throw an exception
        if (rowsAffected == 0)
        {
            throw new DbUpdateException("Failed to update the city in the database.");
        }

        return city;
    }
}
