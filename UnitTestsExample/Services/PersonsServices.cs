using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Services;

public class PersonsServices : IPersonsServices
{
    private readonly List<Person> _persons = [];

    public PersonResponse AddPerson(PersonAddRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(request.FirstName, nameof(request.FirstName));
        ArgumentNullException.ThrowIfNull(request.LastName, nameof(request.LastName));

        Person person = request.ToPerson();
        person.PersonId = Guid.NewGuid();
        _persons.Add(person);
        return person.ToPersonResponse();
    }

    public List<PersonResponse> GetPersons()
    {
        return [.. _persons.Select(p => p.ToPersonResponse())];
    }

    public List<PersonResponse> GetPersons(string filter)
    {
        return
        [
            .. _persons
                .Select(p => p.ToPersonResponse())
                .Where(p =>
                {
                    if (string.IsNullOrEmpty(filter))
                    {
                        return true;
                    }

                    return p.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                        || p.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
                }),
        ];
    }

    public List<PersonResponse> GetPersons(SortOptions sort)
    {
        return sort switch
        {
            SortOptions.Ascending =>
            [
                .. _persons.OrderBy(p => p.FirstName).Select(p => p.ToPersonResponse()),
            ],
            SortOptions.Descending =>
            [
                .. _persons.OrderByDescending(p => p.FirstName).Select(p => p.ToPersonResponse()),
            ],
            _ => [.. _persons.Select(p => p.ToPersonResponse())],
        };
    }

    public PersonResponse? GetPersonById(Guid? personId)
    {
        return _persons.FirstOrDefault(p => p.PersonId == personId)?.ToPersonResponse();
    }
}
