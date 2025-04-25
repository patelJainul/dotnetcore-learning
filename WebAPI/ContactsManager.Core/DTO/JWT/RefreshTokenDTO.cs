using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO.JWT;

public class RefreshTokenDTO
{
    [Required(ErrorMessage = "Token is required.")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Refresh token is required.")]
    public required string RefreshToken { get; set; }
}
