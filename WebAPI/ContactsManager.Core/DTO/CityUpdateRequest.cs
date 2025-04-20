using System.ComponentModel.DataAnnotations;
using CitiesManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO;

public class CityUpdateRequest
{
    [Required(ErrorMessage = "City Name is required.")]
    public string? Name { get; set; }

    public City ToCity(Guid cityId)
    {
        return new City { Name = Name ?? string.Empty, CityId = cityId };
    }
}
