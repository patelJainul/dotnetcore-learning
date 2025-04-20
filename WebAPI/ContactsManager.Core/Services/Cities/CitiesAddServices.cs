using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO.Cities;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CityAddServices(ICityRepository citiesRepository) : ICityAddServices
{
    private readonly ICityRepository _cityRepository = citiesRepository;

    public async Task<CityResponse> AddCityAsync(
        CityAddRequest cityAddRequest,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(cityAddRequest, nameof(cityAddRequest));
        ArgumentException.ThrowIfNullOrEmpty(cityAddRequest.Name, nameof(cityAddRequest.Name));

        return (
            await _cityRepository.AddCityAsync(cityAddRequest.ToCity(), cancellationToken)
        ).ToCityResponse();
    }
}
