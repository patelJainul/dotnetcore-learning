using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityUpdateServices
{
    Task<CityResponse> UpdateCityAsync(
        Guid cityId,
        CityUpdateRequest cityUpdateRequest,
        CancellationToken cancellationToken = default
    );
}
