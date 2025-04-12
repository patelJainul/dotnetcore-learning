using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts.Cities;

namespace ContactsManager.Core.Services.Cities;

public class CityAddServices(ICitiesRepository citiesRepository) : ICityAddServices
{
    private readonly ICitiesRepository _citiesRepository = citiesRepository;

    public async Task<CityResponse> AddCityAsync(
        CityAddRequest cityAddRequest,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(cityAddRequest, nameof(cityAddRequest));
        ArgumentException.ThrowIfNullOrEmpty(cityAddRequest.Name, nameof(cityAddRequest.Name));

        return await _citiesRepository
            .AddCityAsync(cityAddRequest.ToCity(), cancellationToken)
            .ContinueWith(task => task.Result.ToCityResponse(), cancellationToken);
    }
}
