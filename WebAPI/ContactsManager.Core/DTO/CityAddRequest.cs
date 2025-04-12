using CitiesManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO;

public class CityAddRequest
{
    public string? Name { get; set; }

    public City ToCity()
    {
        return new City { Name = Name ?? string.Empty };
    }
}
