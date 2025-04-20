using CitiesManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CityUpdateServices(ICityRepository citiesRepository) : ICityUpdateServices
{
    private readonly ICityRepository _cityRepository = citiesRepository;

    public async Task<CityResponse?> UpdateCityAsync(
        Guid? cityId,
        CityUpdateRequest cityUpdateRequest,
        CancellationToken cancellationToken = default
    )
    {
        // Validate the input parameters
        ArgumentNullException.ThrowIfNull(cityId, nameof(cityId));
        ValidationHelper.GuidValidation(cityId, "City ID is required.");
        ValidationHelper.ModelValidation(cityUpdateRequest);

        City city = cityUpdateRequest.ToCity(cityId.Value);

        var updatedCity = await _cityRepository.UpdateCityAsync(city, cancellationToken);
        return updatedCity?.ToCityResponse();
    }
}
