using System.Security.Claims;
using ContactsManager.Core.DTO.JWT;
using ContactsManager.Core.DTO.Users;
using ContactsManager.Core.Enums;
using ContactsManager.Core.Exceptions;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.Identity;
using ContactsManager.Core.ServiceContracts.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers.v1;

/// <summary>
/// The `AccountController` class is a controller that handles user account-related actions.
/// It provides endpoints for user registration, login, and other account management tasks.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
public class AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationRole> roleManager,
    IAuthServices authServices
) : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IAuthServices _authServices = authServices;

    /// <summary>
    /// Registers a new user in the system.
    /// This endpoint accepts a `RegisterDTO` object containing user information and creates a new user account.
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        try
        {
            ValidationHelper.ModelValidation(model);

            var user = new ApplicationUser
            {
                PersonName = model.PersonName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Phone,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(
                    JsonResponse<List<string>>.ToJsonResponse(
                        [.. result.Errors.Select(e => e.Description)],
                        "User registration failed.",
                        ResponseStatusOptions.Error
                    )
                );
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var authResponse = _authServices.CreateToken(user);
            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return CreatedAtAction(
                nameof(Register),
                JsonResponse<ApplicationUser>.ToJsonResponse(user, "User registered successfully.")
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToJsonResponse());
        }
    }

    /// <summary>
    /// Checks if the provided email already exists in the system.
    /// This endpoint is used to validate the uniqueness of the email during user registration.
    /// </summary>
    /// <param name="email">The email address to check for existence.</param>
    [HttpGet()]
    [AllowAnonymous]
    public async Task<IActionResult> IsEmailAlreadyExist(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToJsonResponse());
        }
    }

    /// <summary>
    /// Logs in a user to the system.
    /// This endpoint accepts a `LoginDTO` object containing user credentials and attempts to authenticate the user.
    ///  </summary>
    ///  <param name="model">The login credentials.</param>

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<JsonResponse<AuthResponse>>> Login(LoginDTO model)
    {
        try
        {
            ValidationHelper.ModelValidation(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );
            if (!result.Succeeded)
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "Invalid email or password.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "User not found.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }

            var authResponse = _authServices.CreateToken(user);
            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return JsonResponse<AuthResponse>.ToJsonResponse(authResponse, "Login successful.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToJsonResponse());
        }
    }

    /// <summary>
    /// Logs out the currently authenticated user.
    /// This endpoint terminates the user's session and clears authentication cookies.
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "User not found.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;
            await _userManager.UpdateAsync(user);

            return Ok(JsonResponse<string>.ToJsonResponse("Logout successful."));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToJsonResponse());
        }
    }

    /// <summary>
    /// Generates a new token for the user using the provided refresh token.
    /// This endpoint accepts a `RefreshTokenDTO` object containing the refresh token and generates a new access token.
    /// </summary>
    /// <param name="model">The refresh token information.</param>
    [HttpPost("GenerateNewToken")]
    [AllowAnonymous]
    public async Task<ActionResult<JsonResponse<AuthResponse>>> GenerateNewToken(
        RefreshTokenDTO model
    )
    {
        try
        {
            ValidationHelper.ModelValidation(model);

            var principal = _authServices.GetPrincipalFromToken(model.Token);
            if (principal == null)
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "Invalid token.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "Invalid token.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (
                user == null
                || user.RefreshToken != model.RefreshToken
                || user.RefreshTokenExpiration <= DateTime.UtcNow
            )
            {
                return BadRequest(
                    JsonResponse<string>.ToJsonResponse(
                        message: "Invalid token.",
                        status: ResponseStatusOptions.Error
                    )
                );
            }

            var authResponse = _authServices.CreateToken(user, model.RefreshToken);
            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return JsonResponse<AuthResponse>.ToJsonResponse(
                data: authResponse,
                message: "New token generated successfully."
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToJsonResponse());
        }
    }
}
