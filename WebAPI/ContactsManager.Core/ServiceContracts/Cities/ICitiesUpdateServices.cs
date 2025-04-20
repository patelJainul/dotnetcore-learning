using ContactsManager.Core.DTO.Cities;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityUpdateServices
{
    Task<CityResponse?> UpdateCityAsync(
        Guid? cityId,
        CityUpdateRequest cityUpdateRequest,
        CancellationToken cancellationToken = default
    );
}
