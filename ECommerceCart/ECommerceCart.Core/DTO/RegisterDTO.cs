using System.ComponentModel.DataAnnotations;
using ECommerceCart.Core.Enums;

namespace ECommerceCart.Core.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
    public required string PersonName { get; set; }

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

    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(
        100,
        ErrorMessage = "Confirm Password must be at least 6 characters long",
        MinimumLength = 6
    )]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public required string ConfirmPassword { get; set; }

    public UserRoleOptions UserRole { get; set; } = UserRoleOptions.User;
}
