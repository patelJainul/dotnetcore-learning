using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO.Users;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
