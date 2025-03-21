using Entities;

namespace ServiceContracts.DTO.UserDto;

public class UserResponse
{
    public Guid UserId { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is not UserResponse)
            return false;

        UserResponse user = (UserResponse)obj;
        return UserId == user.UserId && Name == user.Name && Email == user.Email;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"UserId: {UserId}, Name: {Name}, Email: {Email}";
    }
}

public static class UserExtensions
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
        };
    }
}
