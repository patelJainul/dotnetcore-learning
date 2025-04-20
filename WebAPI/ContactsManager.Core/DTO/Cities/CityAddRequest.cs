using System.ComponentModel.DataAnnotations;
using CitiesManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO.Cities;

public class CityAddRequest
{
    [Required(ErrorMessage = "City Name is required.")]
    [StringLength(100, ErrorMessage = "City Name cannot be longer than 100 characters.")]
    [RegularExpression(
        @"^[a-zA-Z\s]+$",
        ErrorMessage = "City Name can only contain letters and spaces."
    )]
    [MinLength(2, ErrorMessage = "City Name must be at least 2 characters long.")]
    public required string Name { get; set; }

    public City ToCity()
    {
        return new City { Name = Name ?? string.Empty };
    }
}
