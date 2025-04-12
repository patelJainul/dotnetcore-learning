using CitiesManager.Core.Domain.Entities;

namespace ContactsManager.Core.Domain.RepositoryContracts;

public interface ICitiesRepository
{
    Task<City> AddCityAsync(City city, CancellationToken cancellationToken = default);
    Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default);
    Task<List<City>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    );
    Task<City> UpdateCityAsync(City city, CancellationToken cancellationToken = default);
    Task<bool> DeleteCityAsync(Guid id, CancellationToken cancellationToken = default);
    Task<City?> GetCityByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
