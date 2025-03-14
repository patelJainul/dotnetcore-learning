using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ContactsManagerTests;

public class CountriesServicesTests()
{
    private readonly ICountriesServices _countriesServices = new CountriesServices();

    #region AddCountryTestCases

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
    /// The function tests adding a country with a null country name and expects an ArgumentException to be
    /// thrown.
    /// </summary>

    [Fact]
    public void AddCountry_NullCountryName()
    {
        // Arrange
        var request = new CountryAddRequest { CountryName = null };

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
        var request = new CountryAddRequest { CountryName = "India" };
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
        CountryAddRequest addRequest = new() { CountryName = "AddCountry" };

        // Act
        CountryResponse addResponse = _countriesServices.AddCountry(addRequest);
        List<CountryResponse> countries = _countriesServices.GetCountries();

        // Assert
        Assert.NotNull(addResponse);
        Assert.NotEqual(Guid.Empty, addResponse.CountryId);
        Assert.Equal(addRequest.CountryName, addResponse.CountryName);
        Assert.Contains(addResponse, countries);
    }

    #endregion

    #region GetCountriesTestCases

    /// <summary>
    /// The GetCountries_ReturnsEmptyList function in C# asserts that the list of countries returned by a
    /// service is not null and is empty.
    /// </summary>
    [Fact]
    public void GetCountries_ReturnsEmptyList()
    {
        // Act
        var countries = _countriesServices.GetCountries();

        // Assert
        Assert.NotNull(countries);
        Assert.Empty(countries);
    }

    /// <summary>
    /// The function tests the method GetCountries to ensure it returns a list of countries after adding a
    /// country using a service.
    /// </summary>
    [Fact]
    public void GetCountries_ReturnsListOfCountries()
    {
        // Arrange
        List<CountryAddRequest> addRequest =
        [
            new CountryAddRequest { CountryName = "USA" },
            new CountryAddRequest { CountryName = "Canada" },
            new CountryAddRequest { CountryName = "Mexico" },
        ];

        List<CountryResponse> addResponse = [];
        addRequest.ForEach(r =>
        {
            addResponse.Add(_countriesServices.AddCountry(r));
        });

        // Act
        var countries = _countriesServices.GetCountries();

        // Assert
        Assert.NotNull(countries);
        Assert.NotEmpty(countries);
        addResponse.ForEach(r =>
        {
            Assert.Contains(r, countries);
        });
    }

    #endregion

    #region GetCountryByIdTestCases

    /// <summary>
    /// The function tests the behavior of the GetCountryById method when a country is not found.
    /// </summary>
    [Fact]
    public void GetCountryById_CountryNotFound()
    {
        // Arrange
        Guid countryId = Guid.NewGuid();

        // Act
        var country = _countriesServices.GetCountry(countryId);

        // Assert
        Assert.Null(country);
    }

    /// <summary>
    /// The function tests the GetCountryById method to verify that a country is found by adding a country
    /// and then retrieving it.
    /// </summary>
    [Fact]
    public void GetCountryById_CountryFound()
    {
        // Arrange
        var addRequest = new CountryAddRequest { CountryName = "India" };
        var addResponse = _countriesServices.AddCountry(addRequest);

        // Act
        var country = _countriesServices.GetCountry(addResponse.CountryId);

        // Assert
        Assert.NotNull(country);
        Assert.Equal(addResponse, country);
    }

    #endregion
}
