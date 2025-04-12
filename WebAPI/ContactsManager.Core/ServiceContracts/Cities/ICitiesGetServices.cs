using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityGetServices
{
    Task<CityResponse> GetCityByIdAsync(Guid cityId, CancellationToken cancellationToken = default);

    Task<List<CityResponse>> GetAllCitiesAsync(CancellationToken cancellationToken = default);
    Task<List<CityResponse>> GetCitiesByNameAsync(
        string name,
        CancellationToken cancellationToken = default
    );
}
