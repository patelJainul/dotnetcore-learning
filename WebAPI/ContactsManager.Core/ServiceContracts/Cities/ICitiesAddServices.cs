using ContactsManager.Core.DTO.Cities;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityAddServices
{
    Task<CityResponse> AddCityAsync(
        CityAddRequest cityAddRequest,
        CancellationToken cancellationToken = default
    );
}
