using ServiceContracts.DTO;

namespace ServiceContracts;

public interface ICountriesServices
{
    public CountryResponse AddCountry(CountryAddRequest? request);
}
