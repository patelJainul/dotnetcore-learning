using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.UserDto;

public class UserUpdateRequest
{
    [Required(ErrorMessage = "User ID is required")]
    public Guid UserId { get; set; }

    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string? Email { get; set; }

    public User ToUpdatedUser(User user)
    {
        return new User
        {
            UserId = UserId,
            Name = Name ?? user.Name,
            Email = Email ?? user.Email,
            Password = user.Password,
        };
    }
}
