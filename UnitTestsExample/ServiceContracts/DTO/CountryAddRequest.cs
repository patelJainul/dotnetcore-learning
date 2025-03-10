using Entities;

namespace ServiceContracts.DTO;

public class CountryAddRequest
{
    public required string CountryName { get; set; }

    public Country ToCountry()
    {
        return new Country { CountryName = CountryName };
    }
}
