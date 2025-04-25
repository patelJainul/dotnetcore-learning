using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Core.DTO.Users;

public class RegisterDTO
{
    [Required(ErrorMessage = "PersonName is required.")]
    public required string PersonName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Remote(
        action: "IsEmailAlreadyExist",
        controller: "Account",
        ErrorMessage = "Email is already in use."
    )]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Phone is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public required string Phone { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [StringLength(
        100,
        ErrorMessage = "Password must be at least {2} characters long.",
        MinimumLength = 6
    )]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
