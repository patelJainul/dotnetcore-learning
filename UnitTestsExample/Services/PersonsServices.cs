using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helper;
using Services.SeedData;

namespace Services;

public class PersonsServices : IPersonsServices
{
    private readonly List<Person> _persons = [];
    private readonly ICountriesServices _countriesServices = new CountriesServices();

    public PersonsServices(bool isSeeded = true)
    {
        if (isSeeded)
        {
            _persons.AddRange(PersonsMockData.GetPersons());
        }
    }

    private PersonResponse ConvertToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesServices.GetCountryById(person.CountryId)?.CountryName;
        return personResponse;
    }

    public PersonResponse AddPerson(PersonAddRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ValidationHelper.ModelValidation(request);

        if (request.CountryId != null)
        {
            _ =
                _countriesServices.GetCountryById(request.CountryId)
                ?? throw new ArgumentException("Given country id doesn't exist");
        }

        Person person = request.ToPerson();
        person.PersonId = Guid.NewGuid();
        _persons.Add(person);

        return ConvertToPersonResponse(person);
    }

    public List<PersonResponse> GetPersons()
    {
        return [.. _persons.Select(ConvertToPersonResponse)];
    }

    public List<PersonResponse> GetPersons(
        string? searchBy,
        string? searchString,
        string? sortBy,
        SortOptions sortOrder = SortOptions.Ascending
    )
    {
        // Get all persons and convert to response
        var persons = _persons.Select(ConvertToPersonResponse).ToList();

        // Apply search if search parameters are provided
        if (!string.IsNullOrEmpty(searchBy) && !string.IsNullOrEmpty(searchString))
        {
            persons = persons
                .Where(person =>
                    searchBy switch
                    {
                        nameof(PersonResponse.FirstName) => person.FirstName.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ),
                        nameof(PersonResponse.LastName) => person.LastName.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ),
                        nameof(PersonResponse.Email) => person.Email?.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ) ?? false,
                        nameof(PersonResponse.DateOfBirth) => person
                            .DateOfBirth?.ToString()
                            .Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false,
                        nameof(PersonResponse.Age) => person
                            .Age?.ToString()
                            .Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false,
                        nameof(PersonResponse.Gender) => person.Gender?.Equals(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ) ?? false,
                        nameof(PersonResponse.Country) => person.Country?.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ) ?? false,
                        nameof(PersonResponse.Address) => person.Address?.Contains(
                            searchString,
                            StringComparison.OrdinalIgnoreCase
                        ) ?? false,
                        nameof(PersonResponse.ReceiveNewsLetters) => person.ReceiveNewsLetters
                            == (searchString == "true"),
                        _ => true,
                    }
                )
                .ToList();
        }

        // Apply sorting if sortBy is provided
        if (!string.IsNullOrEmpty(sortBy))
        {
            persons = sortBy switch
            {
                nameof(PersonResponse.FirstName) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.FirstName).ToList()
                    : persons.OrderByDescending(p => p.FirstName).ToList(),
                nameof(PersonResponse.LastName) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.LastName).ToList()
                    : persons.OrderByDescending(p => p.LastName).ToList(),
                nameof(PersonResponse.Email) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.Email).ToList()
                    : persons.OrderByDescending(p => p.Email).ToList(),
                nameof(PersonResponse.DateOfBirth) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.DateOfBirth).ToList()
                    : persons.OrderByDescending(p => p.DateOfBirth).ToList(),
                nameof(PersonResponse.Age) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.Age).ToList()
                    : persons.OrderByDescending(p => p.Age).ToList(),
                nameof(PersonResponse.Gender) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.Gender).ToList()
                    : persons.OrderByDescending(p => p.Gender).ToList(),
                nameof(PersonResponse.Country) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.Country).ToList()
                    : persons.OrderByDescending(p => p.Country).ToList(),
                nameof(PersonResponse.Address) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.Address).ToList()
                    : persons.OrderByDescending(p => p.Address).ToList(),
                nameof(PersonResponse.ReceiveNewsLetters) => sortOrder == SortOptions.Ascending
                    ? persons.OrderBy(p => p.ReceiveNewsLetters).ToList()
                    : persons.OrderByDescending(p => p.ReceiveNewsLetters).ToList(),
                _ => persons,
            };
        }

        return persons;
    }

    public PersonResponse? GetPersonById(Guid? personId)
    {
        ArgumentNullException.ThrowIfNull(personId, nameof(personId));

        Person? person = _persons.FirstOrDefault(p => p.PersonId == personId);
        if (person == null)
        {
            return null;
        }

        return ConvertToPersonResponse(person);
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ValidationHelper.ModelValidation(request);

        if (request.CountryId != null)
        {
            _ =
                _countriesServices.GetCountryById(request.CountryId)
                ?? throw new ArgumentException("Given country id doesn't exist");
        }

        Person? person =
            _persons.FirstOrDefault(p => p.PersonId == request.PersonId)
            ?? throw new ArgumentException("Given person id doesn't exist");

        person = request.ToUpdatedPerson(person);

        _persons.RemoveAll(p => p.PersonId == request.PersonId);
        _persons.Add(person);
        return ConvertToPersonResponse(person);
    }

    public PersonResponse DeletePerson(Guid? personId)
    {
        ArgumentNullException.ThrowIfNull(personId, nameof(personId));

        Person? person =
            _persons.FirstOrDefault(p => p.PersonId == personId)
            ?? throw new ArgumentException("Given person id doesn't exist");

        _persons.RemoveAll(p => p.PersonId == personId);
        return ConvertToPersonResponse(person);
    }
}
