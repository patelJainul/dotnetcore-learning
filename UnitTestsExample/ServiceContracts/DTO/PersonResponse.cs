using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonResponse
{
    public Guid PersonId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public Guid? CountryId { get; set; }
    public bool ReceiveNewsLetters { get; set; }

    public double? Age
    {
        get
        {
            if (DateOfBirth.HasValue)
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Value.Year;
                if (DateOfBirth.Value.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
            return null;
        }
    }

    /// <summary>
    /// The Equals method in C# is overridden to compare the properties of two PersonResponse objects for
    /// equality.
    /// </summary>
    /// <param name="obj">The `obj` parameter in the `Equals` method is of type `object` and represents the
    /// object to compare with the current instance for equality. In the provided code snippet, it is being
    /// checked for null and type compatibility before comparing the individual properties of a
    /// `PersonResponse` object with the properties</param>
    /// <returns>
    /// The Equals method is being overridden to compare the current instance of a PersonResponse object
    /// with another object. It checks if the other object is null or not of the same type as
    /// PersonResponse, returning false in those cases. If the objects are of the same type, it then
    /// compares specific properties (PersonId, FirstName, LastName, Email, DateOfBirth, Address, CountryId,
    /// ReceiveNewsLetters)
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is not PersonResponse)
            return false;

        PersonResponse other = (PersonResponse)obj;
        return PersonId == other.PersonId
            && FirstName == other.FirstName
            && LastName == other.LastName
            && Email == other.Email
            && DateOfBirth == other.DateOfBirth
            && Gender == other.Gender
            && CountryId == other.CountryId
            && Country == other.Country
            && ReceiveNewsLetters == other.ReceiveNewsLetters
            && Age == other.Age
            && Address == other.Address;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"PersonId: {PersonId}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, DateOfBirth: {DateOfBirth}, Gender: {Gender}, CountryId: {CountryId}, Country: {Country}, ReceiveNewsLetters: {ReceiveNewsLetters}, Age: {Age}, Address: {Address}";
    }

    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest
        {
            PersonId = PersonId,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender != null ? Enum.Parse<GenderOptions>(Gender) : null,
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetters,
        };
    }
}

public static class PersonResponseExtensions
{
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse
        {
            PersonId = person.PersonId,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            Address = person.Address,
            ReceiveNewsLetters = person.ReceiveNewsLetters,
            CountryId = person.CountryId,
        };
    }
}
