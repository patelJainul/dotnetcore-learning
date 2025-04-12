using CitiesManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO;

public class CityUpdateRequest
{
    public string? Name { get; set; }

    public City ToCity()
    {
        return new City { Name = Name ?? string.Empty };
    }
}
