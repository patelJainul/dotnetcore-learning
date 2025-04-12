using ContactsManager.Core.DTO;
using ContactsManager.Core.Helper;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityAddServices
{
    Task<CityResponse> AddCityAsync(
        CityAddRequest cityAddRequest,
        CancellationToken cancellationToken = default
    );
}
