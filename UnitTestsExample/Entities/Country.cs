using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Country
{
    [Key]
    public Guid CountryId { get; set; }
    public required string CountryName { get; set; }
}
