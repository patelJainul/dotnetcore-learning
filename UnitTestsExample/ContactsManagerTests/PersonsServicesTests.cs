using System;
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
}
