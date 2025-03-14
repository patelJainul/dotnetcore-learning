using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonAddRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public string? Address { get; set; }
    public Guid? CountryId { get; set; }
    public bool ReceiveNewsLetters { get; set; }

    /// <summary>
    /// The ToPerson function creates a new Person object with default values for properties if they are
    /// null.
    /// </summary>
    /// <returns>
    /// A new instance of the `Person` class is being returned with properties initialized based on the
    /// values of the current object's properties. If any of the properties like `FirstName`, `LastName`,
    /// `Email`, `Gender`, `Address` are `null`, they are replaced with an empty string.
    /// </returns>
    public Person ToPerson()
    {
        return new Person
        {
            FirstName = FirstName ?? string.Empty,
            LastName = LastName ?? string.Empty,
            Email = Email ?? string.Empty,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString() ?? string.Empty,
            Address = Address ?? string.Empty,
            CountryId = CountryId,
            ReceiveNewsLetters = ReceiveNewsLetters,
        };
    }
}
