using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CitiesGetServices(ICityRepository citiesRepository) : ICityGetServices
{
    private readonly ICityRepository _cityRepository = citiesRepository;

    public async Task<List<CityResponse>> GetAllCitiesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var cityList = await _cityRepository.GetAllCitiesAsync(cancellationToken);

        return [.. cityList.Select(city => city.ToCityResponse())];
    }

    public async Task<List<CityResponse>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    )
    {
        var cityList = await _cityRepository.GetCitiesByNameAsync(name, cancellationToken);

        return [.. cityList.Select(city => city.ToCityResponse())];
    }

    public async Task<CityResponse?> GetCityByIdAsync(
        Guid? cityId,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(cityId, nameof(cityId));
        if (cityId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(cityId), "City ID cannot be empty.");
        }
        if (cityId == default)
        {
            throw new ArgumentException("City ID cannot be default.", nameof(cityId));
        }
        var city = await _cityRepository.GetCityByIdAsync(cityId.Value, cancellationToken);

        return city?.ToCityResponse();
    }
}
