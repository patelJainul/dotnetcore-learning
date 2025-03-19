using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helper;

namespace Services;

public class PersonsServices : IPersonsServices
{
    private readonly List<Person> _persons = [];
    private readonly ICountriesServices _countriesServices = new CountriesServices();

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

        // if (request.CountryId != null)
        // {
        //     _ =
        //         _countriesServices.GetCountryById(request.CountryId)
        //         ?? throw new ArgumentException("Given country id doesn't exist");
        // }

        Person person = request.ToPerson();
        person.PersonId = Guid.NewGuid();
        _persons.Add(person);

        return ConvertToPersonResponse(person);
    }

    public List<PersonResponse> GetPersons()
    {
        return [.. _persons.Select(ConvertToPersonResponse)];
    }

    public List<PersonResponse> GetPersons(string? searchBy, string? searchString)
    {
        if (string.IsNullOrEmpty(searchBy))
        {
            return [.. _persons.Select(ConvertToPersonResponse)];
        }

        if (string.IsNullOrEmpty(searchString))
        {
            return [.. _persons.Select(ConvertToPersonResponse)];
        }

        return
        [
            .. _persons
                .Select(ConvertToPersonResponse)
                .Where(p =>
                {
                    if (searchBy == "FirstName")
                    {
                        return p.FirstName.Contains(
                            searchString,
                            StringComparison.CurrentCultureIgnoreCase
                        );
                    }

                    if (searchBy == "LastName")
                    {
                        return p.LastName.Contains(
                            searchString,
                            StringComparison.CurrentCultureIgnoreCase
                        );
                    }

                    if (searchBy == "Email")
                    {
                        return p.Email?.Contains(
                                searchString,
                                StringComparison.CurrentCultureIgnoreCase
                            ) ?? false;
                    }

                    if (searchBy == "DateOfBirth")
                    {
                        return p.DateOfBirth?.ToString()
                                .Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                            ?? false;
                    }

                    if (searchBy == "Age")
                    {
                        return p.Age?.ToString()
                                .Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                            ?? false;
                    }

                    if (searchBy == "Gender")
                    {
                        return p.Gender?.Contains(
                                searchString,
                                StringComparison.CurrentCultureIgnoreCase
                            ) ?? false;
                    }

                    if (searchBy == "Country")
                    {
                        return p.Country?.Contains(
                                searchString,
                                StringComparison.CurrentCultureIgnoreCase
                            ) ?? false;
                    }

                    if (searchBy == "Address")
                    {
                        return p.Address?.Contains(
                                searchString,
                                StringComparison.CurrentCultureIgnoreCase
                            ) ?? false;
                    }

                    if (searchBy == "ReceiveNewsLetters")
                    {
                        return p.ReceiveNewsLetters == (searchString == "true");
                    }

                    return true;
                }),
        ];
    }

    public List<PersonResponse> GetPersons(SortOptions sort)
    {
        return sort switch
        {
            SortOptions.Ascending =>
            [
                .. _persons.OrderBy(p => p.FirstName).Select(ConvertToPersonResponse),
            ],
            SortOptions.Descending =>
            [
                .. _persons.OrderByDescending(p => p.FirstName).Select(ConvertToPersonResponse),
            ],
            _ => [.. _persons.Select(ConvertToPersonResponse)],
        };
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
        ArgumentNullException.ThrowIfNull(request.PersonId, nameof(request.PersonId));

        // if (request.CountryId != null)
        // {
        //     _ =
        //         _countriesServices.GetCountryById(request.CountryId)
        //         ?? throw new ArgumentException("Given country id doesn't exist");
        // }

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
