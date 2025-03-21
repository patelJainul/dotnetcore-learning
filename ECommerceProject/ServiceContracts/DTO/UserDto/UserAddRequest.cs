using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.UserDto;

public class UserAddRequest
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Password must be at least 6 characters long"
    )]
    public required string Password { get; set; }

    public User ToUser()
    {
        return new User
        {
            Name = Name,
            Email = Email,
            Password = Password,
        };
    }
}
