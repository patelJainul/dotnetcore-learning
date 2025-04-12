using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CitiesGetServices(ICitiesRepository citiesRepository) : ICityGetServices
{
    private readonly ICitiesRepository _citiesRepository = citiesRepository;

    public Task<List<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
    {
        return _citiesRepository
            .GetAllCitiesAsync(cancellationToken)
            .ContinueWith(
                task => task.Result.Select(city => city.ToCityResponse()).ToList(),
                cancellationToken
            );
    }

    public Task<List<CityResponse>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }

    public Task<CityResponse> GetCityByIdAsync(
        Guid cityId,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }
}
