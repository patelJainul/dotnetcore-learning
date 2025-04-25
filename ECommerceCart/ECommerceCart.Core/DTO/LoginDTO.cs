using System.ComponentModel.DataAnnotations;

namespace ECommerceCart.Core.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(
        100,
        ErrorMessage = "Password must be at least 6 characters long",
        MinimumLength = 6
    )]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
