using ServiceContracts.DTO;

namespace ServiceContracts;

public interface IPersonsServices
{
    /// <summary>
    /// The function AddPerson takes a PersonAddRequest as input and returns a PersonResponse.
    /// </summary>
    /// <param name="request">The `AddPerson` method takes a `PersonAddRequest` object as an optional
    /// parameter and returns a `PersonResponse` object. The `PersonAddRequest` object likely contains
    /// information about a person that needs to be added, such as their name, age, address, etc. The
    /// `Person</param>
    /// <returns>
    /// The `AddPerson` method returns a `PersonResponse` object, which likely contains information about the
    /// person that was added, such as their ID, name, age, address, etc.
    /// </returns>
    public PersonResponse AddPerson(PersonAddRequest? request);

    /// <summary>
    /// This function returns a list of PersonResponse objects.
    /// </summary>
    /// <returns>
    /// The method `GetPersons` returns a list of `PersonResponse` objects.
    /// </returns>
    public List<PersonResponse> GetPersons();

    /// <summary>
    /// This C# function retrieves a person's information by their unique identifier, returning a nullable
    /// PersonResponse object.
    /// </summary>
    /// <param name="personId">The `personId` parameter is a unique identifier for a person, represented as
    /// a `Guid` data type. It is nullable (`Guid?`), meaning it can either have a valid GUID value or be
    /// null.</param>
    /// <returns>
    /// The `GetPersonById` method returns a nullable `PersonResponse` object. If the person with the
    /// specified `personId` is found, the method returns a `PersonResponse` object containing the person's
    /// information. If the person is not found, the method returns `null`.
    /// </returns>
    public PersonResponse? GetPersonById(Guid? personId);
}
