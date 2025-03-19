using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO;

public class CountryAddRequest
{
    [Required(ErrorMessage = "Country Name is required")]
    public string? CountryName { get; set; }

    /// <summary>
    /// The ToCountry function creates a new Country object with the CountryName property set to a non-null
    /// value or an empty string.
    /// </summary>
    /// <returns>
    /// A new instance of the `Country` class is being returned, with the `CountryName` property set to the
    /// value of `CountryName` if it is not null, otherwise it is set to an empty string.
    /// </returns>
    public Country ToCountry()
    {
        return new Country { CountryName = CountryName ?? string.Empty };
    }
}
