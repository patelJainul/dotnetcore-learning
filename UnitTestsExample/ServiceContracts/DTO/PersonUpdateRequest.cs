using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonUpdateRequest
{
    [Required(ErrorMessage = "Person ID is required")]
    public Guid PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public string? Address { get; set; }
    public Guid? CountryId { get; set; }
    public bool? ReceiveNewsLetters { get; set; }

    public Person ToUpdatedPerson(Person person)
    {
        person.FirstName = FirstName ?? person.FirstName;
        person.LastName = LastName ?? person.LastName;
        person.Email = Email ?? person.Email;
        person.DateOfBirth = DateOfBirth ?? person.DateOfBirth;
        person.Gender = Gender?.ToString() ?? person.Gender;
        person.Address = Address ?? person.Address;
        person.CountryId = CountryId ?? person.CountryId;
        person.ReceiveNewsLetters = ReceiveNewsLetters ?? person.ReceiveNewsLetters;
        return person;
    }
}
