using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ContactsManagerTests;

public class CountriesServicesTests()
{
    private readonly ICountriesServices _countriesServices = new CountriesServices();

    /// <summary>
    /// The function tests the behavior of adding a country with a null argument.
    /// </summary>
    [Fact]
    public void AddCountry_NullArgument()
    {
        // Arrange
        CountryAddRequest? request = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() => _countriesServices.AddCountry(request));
    }

    /// <summary>
    /// The function tests adding a country when the country already exists.
    /// </summary>
    [Fact]
    public void AddCountry_CountryAlreadyExists()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = "CountryName" };
        _countriesServices.AddCountry(request);

        // Act
        void act() => _countriesServices.AddCountry(request);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    /// <summary>
    /// The AddCountry_ValidArgument test method verifies that a country can be successfully added with
    /// valid input.
    /// </summary>
    [Fact]
    public void AddCountry_ValidArgument()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = "CountryName" };

        // Act
        var response = _countriesServices.AddCountry(request);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.CountryId);
        Assert.Equal(request.CountryName, response.CountryName);
    }
}
