using System.Security.Claims;
using ContactsManager.Core.DTO.JWT;
using ContactsManager.Core.Identity;

namespace ContactsManager.Core.ServiceContracts.JWT;

public interface IAuthServices
{
    AuthResponse CreateToken(ApplicationUser user, string? refreshToken = null);

    ClaimsPrincipal GetPrincipalFromToken(string token);
}
