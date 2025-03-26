using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesServices(ContactDbContext context) : ICountriesServices
{
    private readonly ContactDbContext _db = context;

    public CountryResponse AddCountry(CountryAddRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.CountryName, "Request or CountryName is null.");

        bool isExist =
            _db.Countries.FirstOrDefault(c => c.CountryName == request.CountryName) != null;
        if (isExist)
        {
            throw new ArgumentException("Country already exists.");
        }

        Country country = request.ToCountry();
        country.CountryId = Guid.NewGuid();
        _db.Countries.Add(country);
        _db.SaveChanges();

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetCountries()
    {
        return [.. _db.Countries.Select(c => c.ToCountryResponse())];
    }

    public CountryResponse? GetCountryById(Guid? countryId)
    {
        return _db.Countries.FirstOrDefault(c => c.CountryId == countryId)?.ToCountryResponse();
    }
}
