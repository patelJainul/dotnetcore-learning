using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ContactsManager.Core.DTO.JWT;
using ContactsManager.Core.Identity;
using ContactsManager.Core.ServiceContracts.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ContactsManager.Core.Services.JWT;

public class AuthServices(IConfiguration configuration) : IAuthServices
{
    private readonly IConfiguration _configuration = configuration;

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public AuthResponse CreateToken(ApplicationUser user, string? refreshToken = null)
    {
        string secretKey =
            _configuration["JWT:SecretKey"]
            ?? throw new InvalidOperationException("JWT:SecretKey is missing in configuration.");
        string issuer =
            _configuration["JWT:Issuer"]
            ?? throw new InvalidOperationException("JWT:Issuer is missing in configuration.");
        string audience =
            _configuration["JWT:Audience"]
            ?? throw new InvalidOperationException("JWT:Audience is missing in configuration.");
        int expirationTimeInMinutes = int.TryParse(
            _configuration["JWT:ExpirationTimeInMinutes"],
            out var expirationTime
        )
            ? expirationTime
            : throw new InvalidOperationException(
                "JWT:ExpirationTimeInMinutes is missing or not a valid integer."
            );

        int refreshTokenExpirationInMinuets = int.TryParse(
            _configuration["RefreshToken:ExpirationTimeInMinutes"],
            out var refreshTokenExpiration
        )
            ? refreshTokenExpiration
            : throw new InvalidOperationException(
                "RefreshToken:ExpirationTimeInMinutes is missing or not a valid integer."
            );

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var expires = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.PersonName ?? string.Empty),
                ]
            ),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthResponse
        {
            PersonName = user.PersonName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Token = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken ?? GenerateRefreshToken(),
            RefreshTokenExpiration =
                refreshToken != null
                    ? user.RefreshTokenExpiration
                    : DateTime.UtcNow.AddMinutes(refreshTokenExpirationInMinuets),
            Expiration = token.ValidTo,
        };
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(
            _configuration["JWT:SecretKey"]
                ?? throw new InvalidOperationException("JWT:SecretKey is missing in configuration.")
        );
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JWT:Audience"],
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
        };
        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken
        );

        if (
            securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
        {
            throw new SecurityTokenException("Invalid token.");
        }
        return principal;
    }
}
