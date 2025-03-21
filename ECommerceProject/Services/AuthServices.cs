using ServiceContracts;
using ServiceContracts.DTO.AuthDto;
using ServiceContracts.DTO.UserDto;
using Services.Helpers;

namespace Services;

public class AuthServices(IUserServices userServices) : IAuthServices
{
    private readonly IUserServices _userServices = userServices;

    public AuthResponse LogIn(AuthRequest? authRequest)
    {
        ArgumentNullException.ThrowIfNull(authRequest);
        ValidationHelper.ModelValidation(authRequest);

        var user =
            _userServices.GetUserByEmail(authRequest.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password");

        if (user.Password != authRequest.Password)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        return new AuthResponse
        {
            Token = "1234567890",
            RefreshToken = "1234567890",
            User = user.ToUserResponse(),
        };
    }
}
