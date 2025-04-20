using CitiesManager.Core.Domain.Entities;
using ContactsManager.Core.Helpers;

namespace ContactsManager.Core.Domain.RepositoryContracts;

public interface ICityRepository
{
    Task<City> AddCityAsync(City city, CancellationToken cancellationToken = default);
    Task<List<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default);
    Task<List<City>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    );
    Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken = default);
    Task<JsonResponse<bool>> DeleteCityAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
    Task<City?> GetCityByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
