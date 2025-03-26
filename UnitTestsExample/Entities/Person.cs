using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Person
{
    [Key]
    public Guid PersonId { get; set; }

    [StringLength(40)]
    public required string FirstName { get; set; }

    [StringLength(40)]
    public required string LastName { get; set; }

    [EmailAddress]
    [StringLength(50)]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [ForeignKey(nameof(Country))]
    public Guid? CountryId { get; set; }
    public bool ReceiveNewsLetters { get; set; }
}
