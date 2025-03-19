using ServiceContracts.DTO;
using ServiceContracts.Enums;

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
    /// The `GetPersons` method returns a list of `PersonResponse` objects. Each `PersonResponse` object likely
    /// contains information about a person, such as their ID, name, age, address, etc.
    /// </returns>
    public List<PersonResponse> GetPersons();

    /// <summary>
    /// This C# function `GetPersons` returns a list of `PersonResponse` objects based on a specified
    /// filter.
    /// </summary>
    /// <param name="filter">The `filter` parameter in the `GetPersons` method is used to specify the
    /// criteria or condition based on which the list of persons will be filtered or retrieved. This
    /// parameter allows you to pass in a string value that can be used to filter the list of persons based
    /// on certain attributes or properties.</param>
    /// <returns>
    /// The `GetPersons` method returns a list of `PersonResponse` objects based on the specified filter
    /// criteria. If no filter is provided (i.e., the `filter` parameter is null or empty), the method
    /// returns the entire list of persons. If a filter is provided, the method returns only those persons
    /// that match the filter criteria.
    /// </returns>
    public List<PersonResponse> GetPersons(string filter);

    /// <summary>
    /// This C# function `GetPersons` returns a list of `PersonResponse` objects based on the specified
    /// sorting options.
    /// </summary>
    /// <param name="SortOptions">SortOptions is an enum that represents different options for sorting the
    /// list of persons. It could include options such as sorting by name, age, date of birth, etc.</param>
    /// <returns>
    /// The `GetPersons` method returns a list of `PersonResponse` objects based on the specified sorting
    /// options. The method sorts the list of persons according to the specified criteria (e.g., ascending or
    /// descending order) and returns the sorted list.
    /// </returns>
    public List<PersonResponse> GetPersons(SortOptions sort);

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

    /// <summary>
    /// The UpdatePerson function in C# updates a person based on the provided request.
    /// </summary>
    /// <param name="request">The `PersonUpdateRequest` parameter is a request object that contains the data
    /// needed to update a person's information. It is marked as nullable with the `?` symbol, which means
    /// it can accept a null value.</param>
    /// <returns>
    /// The `UpdatePerson` method returns a `PersonResponse` object, which likely contains information about the
    /// person that was updated, such as their ID, name, age, address, etc.
    /// </returns>
    public PersonResponse UpdatePerson(PersonUpdateRequest? request);

    /// <summary>
    /// The DeletePerson function in C# deletes a person record based on the provided personId.
    /// </summary>
    /// <param name="personId">The `DeletePerson` method takes a parameter `personId` of type `Guid?`, which
    /// means it is a nullable `Guid` type. This parameter is used to identify the person that needs to be
    /// deleted from the system.</param>
    /// <returns>
    /// The `DeletePerson` method returns a `PersonResponse` object, which likely contains information about the
    /// person that was deleted, such as their ID, name, age, address, etc.
    /// </returns>
    public PersonResponse DeletePerson(Guid? personId);
}
