using ServiceContracts.DTO;

namespace ServiceContracts;

public interface ICountriesServices
{
    /// <summary>
    /// The AddCountry function in C# adds a new country based on the provided request.
    /// </summary>
    /// <param name="request">The `AddCountry` method takes a `CountryAddRequest` object as a parameter,
    /// which is nullable (`CountryAddRequest?`). This object likely contains the data needed to add a
    /// new country to the system. The method returns a `CountryResponse` object, which may contain
    /// information about the success</param>
    /// <returns>
    /// The method `AddCountry` returns a `CountryResponse` object.
    /// </returns>

    public CountryResponse AddCountry(CountryAddRequest? request);

    /// <summary>
    /// This function returns a list of CountryResponse objects.
    /// </summary>
    /// <returns>
    /// The method `GetCountries` returns a list of `CountryResponse` objects.
    /// </returns>
    public List<CountryResponse> GetCountries();

    /// <summary>
    /// This C# function retrieves information about a country based on its unique identifier.
    /// </summary>
    /// <param name="Guid">A Guid is a globally unique identifier, which is a 128-bit integer value
    /// typically represented as a 32-character hexadecimal string. It is often used in programming to
    /// uniquely identify objects or entities.</param>
    /// <returns>
    /// The method `GetCountry` returns a `CountryResponse` object.
    /// </returns>
    public CountryResponse? GetCountryById(Guid? countryId);
}
