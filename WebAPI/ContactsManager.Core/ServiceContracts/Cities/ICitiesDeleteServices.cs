using ContactsManager.Core.Helpers;

namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityDeleteServices
{
    Task<JsonResponse<bool>> DeleteCityAsync(
        Guid? cityId,
        CancellationToken cancellationToken = default
    );
}
