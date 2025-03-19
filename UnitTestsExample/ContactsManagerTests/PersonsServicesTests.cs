using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace ContactsManagerTests;

public class PersonsServicesTests(ITestOutputHelper testOutputHelper)
{
    private readonly ICountriesServices _countriesServices = new CountriesServices();
    private readonly IPersonsServices _personsServices = new PersonsServices(isSeeded: false);

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
    [Fact]
    public void AddPerson_InvalidCountryId()
    {
        // Arrange
        var request = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            CountryId = Guid.NewGuid(),
        };

        // Act
        void act() => _personsServices.AddPerson(request);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    /// <summary>
    /// The `AddPerson_ValidRequest` function tests the successful addition of a person with valid input
    /// data.
    /// </summary>
    [Fact]
    public void AddPerson_ValidRequest()
    {
        // Arrange
        var country = _countriesServices.GetCountries().First();

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
        var country = _countriesServices.GetCountries().First();

        var country2 = _countriesServices.GetCountries().Skip(1).First();

        var country3 = _countriesServices.GetCountries().Skip(2).First();

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
        var country = _countriesServices.GetCountries().First();

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
        var persons = _personsServices.GetPersons(
            searchBy: nameof(PersonResponse.FirstName),
            searchString: filter,
            sortBy: null,
            sortOrder: SortOptions.Ascending
        );

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
        var country = _countriesServices.GetCountries().First();

        var country2 = _countriesServices.GetCountries().Skip(1).First();

        var country3 = _countriesServices.GetCountries().Skip(2).First();

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

        // Act

        // Filter by first name
        var filteredPersonsByFirstName = _personsServices.GetPersons(
            searchBy: nameof(PersonResponse.FirstName),
            searchString: "oh",
            sortBy: null,
            sortOrder: SortOptions.Ascending
        );

        var expectedFilteredPersonsByFirstName = responseList.Where(p =>
            p.FirstName.Contains("oh", StringComparison.OrdinalIgnoreCase)
        );

        _testOutputHelper.WriteLine("Actual Filtered Persons by First Name:");
        foreach (var person in filteredPersonsByFirstName)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }
        _testOutputHelper.WriteLine("Expected Filtered Persons by First Name:");
        foreach (var person in expectedFilteredPersonsByFirstName)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Filter by last name
        var filteredPersonsByLastName = _personsServices.GetPersons(
            searchBy: nameof(PersonResponse.LastName),
            searchString: "mi",
            sortBy: null,
            sortOrder: SortOptions.Ascending
        );

        var expectedFilteredPersonsByLastName = responseList.Where(p =>
            p.LastName.Contains("mi", StringComparison.OrdinalIgnoreCase)
        );

        _testOutputHelper.WriteLine("Actual Filtered Persons by Last Name:");
        foreach (var person in filteredPersonsByLastName)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        _testOutputHelper.WriteLine("Expected Filtered Persons by Last Name:");
        foreach (var person in expectedFilteredPersonsByLastName)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        Assert.NotNull(filteredPersonsByFirstName);
        Assert.NotEmpty(filteredPersonsByFirstName);
        Assert.Equal(expectedFilteredPersonsByFirstName, filteredPersonsByFirstName);

        Assert.NotNull(filteredPersonsByLastName);
        Assert.NotEmpty(filteredPersonsByLastName);
        Assert.Equal(expectedFilteredPersonsByLastName, filteredPersonsByLastName);
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
        var country = _countriesServices.GetCountries().First();

        var country2 = _countriesServices.GetCountries().Skip(1).First();

        var country3 = _countriesServices.GetCountries().Skip(2).First();

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

        // Ascending
        // Act
        var personsAscending = _personsServices.GetPersons(
            searchBy: null,
            searchString: null,
            sortBy: nameof(PersonResponse.FirstName),
            sortOrder: SortOptions.Ascending
        );

        var expectedPersonsAscending = responseList.OrderBy(p => p.FirstName);

        _testOutputHelper.WriteLine("Actual Persons Ascending:");
        foreach (var person in personsAscending)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        _testOutputHelper.WriteLine("Expected Persons Ascending:");
        foreach (var person in expectedPersonsAscending)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        Assert.NotNull(personsAscending);
        Assert.NotEmpty(personsAscending);
        Assert.Equal(expectedPersonsAscending, personsAscending);

        // Descending
        // Act
        var personsDescending = _personsServices.GetPersons(
            searchBy: null,
            searchString: null,
            sortBy: nameof(PersonResponse.FirstName),
            sortOrder: SortOptions.Descending
        );

        var expectedPersonsDescending = responseList.OrderByDescending(p => p.FirstName);

        _testOutputHelper.WriteLine("Actual Persons Descending:");
        foreach (var person in personsDescending)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        _testOutputHelper.WriteLine("Expected Persons Descending:");
        foreach (var person in expectedPersonsDescending)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        // Assert
        Assert.NotNull(personsDescending);
        Assert.NotEmpty(personsDescending);
        Assert.Equal(expectedPersonsDescending, personsDescending);
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
    [Fact]
    public void UpdatePerson_InvalidCountryId()
    {
        // Arrange
        var addRequest = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            CountryId = Guid.NewGuid(),
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
        };

        void act() => _personsServices.AddPerson(addRequest);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

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

        var country = _countriesServices.GetCountries().First();

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
        var country = _countriesServices.GetCountries().First();

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
