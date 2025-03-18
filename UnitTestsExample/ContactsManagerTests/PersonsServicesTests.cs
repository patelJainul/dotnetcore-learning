using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace ContactsManagerTests;

public class PersonsServicesTests
{
    private readonly IPersonsServices _personsServices = new PersonsServices();

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
        Assert.Throws<ArgumentNullException>(() => _personsServices.AddPerson(request));
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
        Assert.Throws<ArgumentNullException>(() => _personsServices.AddPerson(request));
    }

    /// <summary>
    /// The `AddPerson_ValidRequest` function tests the successful addition of a person with valid input
    /// data.
    /// </summary>
    [Fact]
    public void AddPerson_ValidRequest()
    {
        // Arrange
        var request = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@gmail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true,
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
        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
        var persons = _personsServices.GetPersons();

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
        var addRequest = new PersonAddRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "1234 Elm Street",
            CountryId = Guid.NewGuid(),
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
        var persons = _personsServices.GetPersons(filter);

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
        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
        var filteredPersonsByFirstName = _personsServices.GetPersons("John");
        var filteredPersonsByLastName = _personsServices.GetPersons("Smith");

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
        // Arrange}
        List<PersonAddRequest> requestList =
        [
            new PersonAddRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@mail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Address = "1234 Elm Street",
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
                CountryId = Guid.NewGuid(),
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
}
