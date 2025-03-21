using ServiceContracts.DTO.UserDto;

namespace ServiceContracts.DTO.AuthDto;

public class AuthResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }

    public required UserResponse User { get; set; }
}
