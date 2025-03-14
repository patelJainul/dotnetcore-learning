using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesServices : ICountriesServices
{
    private readonly List<Country> _countries = [];

    public CountryResponse AddCountry(CountryAddRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.CountryName, "Request or CountryName is null.");

        bool isExist = _countries.Find(c => c.CountryName == request.CountryName) != null;
        if (isExist)
        {
            throw new ArgumentException("Country already exists.");
        }

        Country country = request.ToCountry();
        country.CountryId = Guid.NewGuid();
        _countries.Add(country);

        return country.ToCountryResponse();
    }
}
