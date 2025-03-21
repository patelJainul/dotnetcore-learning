using ServiceContracts.DTO.AuthDto;

namespace ServiceContracts;

public interface IAuthServices
{
    public AuthResponse LogIn(AuthRequest? authRequest);
}
