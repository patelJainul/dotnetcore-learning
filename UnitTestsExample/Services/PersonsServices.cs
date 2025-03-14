using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

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

    public PersonResponse? GetPersonById(Guid? personId)
    {
        return _persons.FirstOrDefault(p => p.PersonId == personId)?.ToPersonResponse();
    }
}
