namespace ContactsManager.Core.ServiceContracts.Cities;

public interface ICityDeleteServices
{
    Task<bool> DeleteCityAsync(Guid cityId, CancellationToken cancellationToken = default);
}
