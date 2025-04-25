namespace ContactsManager.Core.DTO.JWT;

public class AuthResponse
{
    public required string PersonName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}
