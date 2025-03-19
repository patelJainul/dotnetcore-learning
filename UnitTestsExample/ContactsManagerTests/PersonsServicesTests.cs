using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace ContactsManagerTests;

public class PersonsServicesTests(ITestOutputHelper testOutputHelper)
{
    private readonly ICountriesServices _countriesServices = new CountriesServices();
    private readonly IPersonsServices _personsServices = new PersonsServices();

    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    #region AddPersonTestCases

    /// <summary>
    /// The function tests for throwing an ArgumentNullException when adding a person with a null argument.
    /// </summary>
    [Fact]
    public void AddPerson_NullArgument()
    {
        // Arrange
        PersonAddRequest? request = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() => _personsServices.AddPerson(request));
    }

    /// <summary>
    /// The function tests adding a person with a null first name and expects an ArgumentNullException to be
    /// thrown.
    /// </summary>
    [Fact]
    public void AddPerson_NullFirstName()
    {
        // Arrange
        var request = new PersonAddRequest { FirstName = null };

        // Assert
        Assert.Throws<ArgumentException>(() => _personsServices.AddPerson(request));
    }

    /// <summary>
    /// The function tests adding a person with a null last name input.
    /// </summary>
    [Fact]
    public void AddPerson_NullLastName()
    {
        // Arrange
        var request = new PersonAddRequest { LastName = null };

        // Assert
        Assert.Throws<ArgumentException>(() => _personsServices.AddPerson(request));
    }

    /// <summary>
    /// The function `AddPerson_InvalidCountryId` tests adding a person with an invalid country ID.
    /// </summary>
    // [Fact]
    // public void AddPerson_InvalidCountryId()
    // {
    //     // Arrange
    //     var request = new PersonAddRequest
    //     {
    //         FirstName = "John",
    //         LastName = "Doe",
    //         Email = "johndoe@mail.com",
    //         DateOfBirth = new DateTime(1990, 1, 1),
    //         CountryId = Guid.NewGuid(),
    //     };

    //     // Act
    //     void act() => _personsServices.AddPerson(request);

    //     // Assert
    //     Assert.Throws<ArgumentException>(act);
    // }

    /// <summary>
    /// The `AddPerson_ValidRequest` function tests the successful addition of a person with valid input
    /// data.
    /// </summary>
    [Fact]
    public void AddPerson_ValidRequest()
    {
        // Arrange
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var request = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@gmail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
            CountryId = country.CountryId,
        };

        // Act
        var response = _personsServices.AddPerson(request);

        var expectedResponse = _personsServices.GetPersonById(response.PersonId);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(expectedResponse);
        Assert.Equal(expectedResponse, response);
    }

    #endregion

    #region GetPersonsTestCases

    /// <summary>
    /// The function GetPersons_ReturnEmptyList tests that the GetPersons method returns an empty list.
    /// </summary>
    [Fact]
    public void GetPersons_ReturnEmptyList()
    {
        // Act
        var persons = _personsServices.GetPersons();

        // Assert
        Assert.NotNull(persons);
        Assert.Empty(persons);
    }

    /// <summary>
    /// The function `GetPersons_ReturnsListOfPersons` tests the retrieval of a list of persons by adding
    /// mock person data and comparing the response with the expected list.
    /// </summary>
    [Fact]
    public void GetPersons_ReturnsListOfPersons()
    {
        // Arrange
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var country2 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "Canada" }
        );

        var country3 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United Kingdom" }
        );

        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country2.CountryId,
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "johnsmith@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country3.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false,
            },
        ];

        List<PersonResponse> responseList = [];
        requestList.ForEach(person =>
        {
            responseList.Add(_personsServices.AddPerson(person));
        });

        _testOutputHelper.WriteLine("Actual Persons:");
        foreach (var person in responseList)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Act
        var persons = _personsServices.GetPersons();

        _testOutputHelper.WriteLine("Expected Persons:");
        foreach (var person in persons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        Assert.NotNull(persons);
        Assert.NotEmpty(persons);
        Assert.Equal(responseList, persons);
    }

    #endregion

    #region GetPersonByIdTestCases

    /// <summary>
    /// The function tests for the scenario where a person is not found by their ID.
    /// </summary>
    [Fact]
    public void GetPersonById_PersonNotFound()
    {
        // Arrange
        Guid personId = Guid.NewGuid();

        // Act
        var person = _personsServices.GetPersonById(personId);

        // Assert
        Assert.Null(person);
    }

    /// <summary>
    /// The function `GetPersonById_PersonFound` tests the retrieval of a person by ID and asserts that the
    /// retrieved person matches the added person.
    /// </summary>
    [Fact]
    public void GetPersonById_PersonFound()
    {
        // Arrange
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var addRequest = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            CountryId = country.CountryId,
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
        };

        var addResponse = _personsServices.AddPerson(addRequest);

        // Act
        var person = _personsServices.GetPersonById(addResponse.PersonId);

        // Assert
        Assert.NotNull(person);
        Assert.Equal(addResponse, person);
    }

    #endregion

    #region GetFilteredPersonsTestCases

    /// <summary>
    /// The function `GetFilteredPersons_ReturnEmptyList` tests that the `GetPersons` method returns an
    /// empty list when filtering for a specific name.
    /// </summary>
    [Fact]
    public void GetFilteredPersons_ReturnEmptyList()
    {
        // Arrange
        var filter = "John";

        // Act
        var persons = _personsServices.GetPersons(searchBy: "FirstName", searchString: filter);

        // Assert
        Assert.NotNull(persons);
        Assert.Empty(persons);
    }

    /// <summary>
    /// The function `GetFilteredPersons_ReturnsListOfPersons` creates a list of person requests, adds
    /// them using a service, and then filters and asserts the results based on first and last names.
    /// </summary>
    [Fact]
    public void GetFilteredPersons_ReturnsListOfPersons()
    {
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var country2 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "Canada" }
        );

        var country3 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United Kingdom" }
        );

        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country2.CountryId,
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "johnsmith@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country3.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false,
            },
        ];

        List<PersonResponse> responseList = [];

        // Act
        requestList.ForEach(r =>
        {
            responseList.Add(_personsServices.AddPerson(r));
        });
        var filteredPersonsByFirstName = _personsServices.GetPersons(
            searchBy: "FirstName",
            searchString: "John"
        );
        var filteredPersonsByLastName = _personsServices.GetPersons(
            searchBy: "LastName",
            searchString: "Smith"
        );

        // Assert
        Assert.NotNull(filteredPersonsByFirstName);
        Assert.NotEmpty(filteredPersonsByFirstName);
        Assert.Equal(
            responseList.Where(p =>
                p.FirstName.Contains("John", StringComparison.CurrentCultureIgnoreCase)
            ),
            filteredPersonsByFirstName
        );

        Assert.NotNull(filteredPersonsByLastName);
        Assert.NotEmpty(filteredPersonsByLastName);
        Assert.Equal(
            responseList.Where(p =>
                p.LastName.Contains("Smith", StringComparison.CurrentCultureIgnoreCase)
            ),
            filteredPersonsByLastName
        );
    }

    #endregion

    #region GetSortedPersonsTestCases

    /// <summary>
    /// The function `GetSortedPersons_ReturnsListOfPersons` arranges a list of `PersonAddRequest`, adds
    /// them as persons, and then asserts the sorting of the resulting `PersonResponse` list in ascending
    /// and descending order.
    /// </summary>
    [Fact]
    public void GetSortedPersons_ReturnsListOfPersons()
    {
        // Arrange
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var country2 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "Canada" }
        );

        var country3 = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United Kingdom" }
        );

        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country2.CountryId,
                Gender = GenderOptions.Female,
                ReceiveNewsLetters = true,
            },
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "johnsmith@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = country3.CountryId,
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false,
            },
        ];

        List<PersonResponse> responseList = [];

        // Act
        requestList.ForEach(person =>
        {
            responseList.Add(_personsServices.AddPerson(person));
        });

        var personsAscending = _personsServices.GetPersons(SortOptions.Ascending);
        var personsDescending = _personsServices.GetPersons(SortOptions.Descending);

        // Assert
        Assert.NotNull(personsAscending);
        Assert.NotEmpty(personsAscending);
        Assert.Equal(responseList.OrderBy(p => p.FirstName), personsAscending);

        Assert.NotNull(personsDescending);
        Assert.NotEmpty(personsDescending);
        Assert.Equal(responseList.OrderByDescending(p => p.FirstName), personsDescending);
    }

    #endregion

    #region UpdatePersonTestCases

    /// <summary>
    /// The function tests for the scenario where a person is not found by their ID.
    /// </summary>
    [Fact]
    public void UpdatePerson_PersonNotFound()
    {
        // Arrange
        var updateRequest = new PersonUpdateRequest { PersonId = Guid.NewGuid() };

        // Act
        void act() => _personsServices.UpdatePerson(updateRequest);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    /// <summary>
    /// The function `UpdatePerson_InvalidCountryId` tests adding a person with an invalid country ID.
    /// </summary>
    // [Fact]
    // public void UpdatePerson_InvalidCountryId()
    // {
    //     // Arrange
    //     var addRequest = new PersonAddRequest
    //     {
    //         FirstName = "John",
    //         LastName = "Doe",
    //         Email = "johndoe@mail.com",
    //         DateOfBirth = new DateTime(1990, 1, 1),
    //         CountryId = Guid.NewGuid(),
    //         Gender = GenderOptions.Male,
    //         ReceiveNewsLetters = true,
    //     };

    //     void act() => _personsServices.AddPerson(addRequest);

    //     // Assert
    //     Assert.Throws<ArgumentException>(act);
    // }

    /// <summary>
    /// The function tests for the scenario where a person is updated with a valid request.
    /// </summary>
    [Fact]
    public void UpdatePerson_ValidRequest()
    {
        // Arrange
        var addRequest = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
        };

        var person = _personsServices.AddPerson(addRequest);

        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        addRequest.CountryId = country.CountryId;

        var updateRequest = new PersonUpdateRequest
        {
            PersonId = person.PersonId,
            FirstName = "Jane",
            LastName = "Doe",
        };

        // Act
        var updatedPerson = _personsServices.UpdatePerson(updateRequest);
        var expectedPerson = _personsServices.GetPersonById(person.PersonId);

        // Assert
        Assert.Equal(expectedPerson, updatedPerson);
    }

    #endregion

    #region DeletePersonTestCases

    /// <summary>
    /// The function tests for the scenario where a person is not found by their ID.
    /// </summary>
    [Fact]
    public void DeletePerson_PersonNotFound()
    {
        // Arrange
        Guid personId = Guid.NewGuid();

        // Act
        void act() => _personsServices.DeletePerson(personId);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    /// <summary>
    /// The function tests for the scenario where a person is deleted with a valid request.
    /// </summary>
    [Fact]
    public void DeletePerson_ValidRequest()
    {
        // Arrange
        var country = _countriesServices.AddCountry(
            new CountryAddRequest { CountryName = "United States" }
        );

        var addRequest = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            CountryId = country.CountryId,
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
        };

        var person = _personsServices.AddPerson(addRequest);

        // Act
        var deletedPerson = _personsServices.DeletePerson(person.PersonId);

        // Assert
        Assert.Equal(person, deletedPerson);
    }

    #endregion
}
