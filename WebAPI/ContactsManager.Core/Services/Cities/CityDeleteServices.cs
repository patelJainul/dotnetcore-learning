using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CityDeleteServices(ICityRepository citiesRepository) : ICityDeleteServices
{
    private readonly ICityRepository _cityRepository = citiesRepository;

    public Task<JsonResponse<bool>> DeleteCityAsync(
        Guid? cityId,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(cityId, nameof(cityId));
        ValidationHelper.GuidValidation(cityId, "City ID is required.");

        return _cityRepository.DeleteCityAsync(cityId.Value, cancellationToken);
    }
}
