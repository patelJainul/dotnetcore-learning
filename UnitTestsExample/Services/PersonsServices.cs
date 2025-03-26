using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helper;

namespace Services;

public class PersonsServices(ContactDbContext context) : IPersonsServices
{
    private readonly ContactDbContext _db = context;
    private readonly ICountriesServices _countriesServices = new CountriesServices(context);

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
        _db.InsertPerson(person);

        return ConvertToPersonResponse(person);
    }

    public List<PersonResponse> GetPersons()
    {
        return [.. _db.GetPersonsStoredProcedure().Select(ConvertToPersonResponse)];
    }

    public List<PersonResponse> GetPersons(
        string? searchBy,
        string? searchString = "",
        string? sortBy = nameof(PersonResponse.FirstName),
        SortOptions sortOrder = SortOptions.Ascending
    )
    {
        // Get all persons and convert to response
        var persons = _db
            .Persons.Select(ConvertToPersonResponse)
            .Where(person =>
                searchBy switch
                {
                    nameof(PersonResponse.FirstName) => person.FirstName.Contains(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ),
                    nameof(PersonResponse.LastName) => person.LastName.Contains(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ),
                    nameof(PersonResponse.Email) => person.Email?.Contains(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ) ?? false,
                    nameof(PersonResponse.DateOfBirth) => person
                        .DateOfBirth?.ToString()
                        .Contains(searchString ?? "", StringComparison.OrdinalIgnoreCase) ?? false,
                    nameof(PersonResponse.Age) => person
                        .Age?.ToString()
                        .Contains(searchString ?? "", StringComparison.OrdinalIgnoreCase) ?? false,
                    nameof(PersonResponse.Gender) => person.Gender?.Equals(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ) ?? false,
                    nameof(PersonResponse.Country) => person.Country?.Contains(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ) ?? false,
                    nameof(PersonResponse.Address) => person.Address?.Contains(
                        searchString ?? "",
                        StringComparison.OrdinalIgnoreCase
                    ) ?? false,
                    nameof(PersonResponse.ReceiveNewsLetters) => person.ReceiveNewsLetters
                        == (searchString ?? "").Equals("true"),
                    _ => true,
                }
            )
            .ToList();

        // Apply sorting if sortBy is provided
        if (!string.IsNullOrEmpty(sortBy))
        {
            persons = sortBy switch
            {
                $"{nameof(PersonResponse.FirstName)},{nameof(PersonResponse.LastName)}" => sortOrder
                == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.FirstName)]
                    : [.. persons.OrderByDescending(p => p.FirstName)],
                nameof(PersonResponse.LastName) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.LastName)]
                    : [.. persons.OrderByDescending(p => p.LastName)],
                nameof(PersonResponse.Email) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.Email)]
                    : [.. persons.OrderByDescending(p => p.Email)],
                nameof(PersonResponse.DateOfBirth) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.DateOfBirth)]
                    : [.. persons.OrderByDescending(p => p.DateOfBirth)],
                nameof(PersonResponse.Age) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.Age)]
                    : [.. persons.OrderByDescending(p => p.Age)],
                nameof(PersonResponse.Gender) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.Gender)]
                    : [.. persons.OrderByDescending(p => p.Gender)],
                nameof(PersonResponse.Country) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.Country)]
                    : [.. persons.OrderByDescending(p => p.Country)],
                nameof(PersonResponse.Address) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.Address)]
                    : [.. persons.OrderByDescending(p => p.Address)],
                nameof(PersonResponse.ReceiveNewsLetters) => sortOrder == SortOptions.Ascending
                    ? [.. persons.OrderBy(p => p.ReceiveNewsLetters)]
                    : [.. persons.OrderByDescending(p => p.ReceiveNewsLetters)],
                _ => persons,
            };
        }

        return persons;
    }

    public PersonResponse? GetPersonById(Guid? personId)
    {
        ArgumentNullException.ThrowIfNull(personId, nameof(personId));

        Person? person = _db.Persons.FirstOrDefault(p => p.PersonId == personId);
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
            _db.Persons.FirstOrDefault(p => p.PersonId == request.PersonId)
            ?? throw new ArgumentException("Given person id doesn't exist");

        person = request.ToUpdatedPerson(person);
        _db.Persons.Update(person);
        _db.SaveChanges();
        return ConvertToPersonResponse(person);
    }

    public PersonResponse DeletePerson(Guid? personId)
    {
        ArgumentNullException.ThrowIfNull(personId, nameof(personId));

        Person? person =
            _db.Persons.FirstOrDefault(p => p.PersonId == personId)
            ?? throw new ArgumentException("Given person id doesn't exist");

        _db.Persons.Remove(person);
        _db.SaveChanges();
        return ConvertToPersonResponse(person);
    }
}
