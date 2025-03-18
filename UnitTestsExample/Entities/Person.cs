namespace Entities;

public class Person
{
    public Guid PersonId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public Guid? CountryId { get; set; }
    public bool ReceiveNewsLetters { get; set; }
}
