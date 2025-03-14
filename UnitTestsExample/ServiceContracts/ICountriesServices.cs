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
}
