using System.ComponentModel.DataAnnotations;
using ModelsExample.CustomValidators;

namespace ModelsExample.Models;

public class Person
{
    [Required]
    public string? FirstName { get; set; } = "Test";

    [Required]
    public string? LastName { get; set; }

    [DateRangeValidator]
    public DateTime? BirthDate { get; set; } = DateTime.Now;

}
