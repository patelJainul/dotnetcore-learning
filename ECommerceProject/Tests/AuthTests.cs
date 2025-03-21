using Entities;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO.AuthDto;
using ServiceContracts.DTO.UserDto;
using Services;

namespace Tests;

public class AuthTests
{
    private readonly Mock<IUserServices> _mockUserServices;
    private readonly IAuthServices _authServices;
    private readonly string _validEmail = "test@example.com";
    private readonly string _validPassword = "password123";
    private readonly Guid _userId = Guid.NewGuid();

    public AuthTests()
    {
        _mockUserServices = new Mock<IUserServices>();

        // Setup for valid user
        var user = new User
        {
            UserId = _userId,
            Name = "Test User",
            Email = _validEmail,
            Password = _validPassword,
        };

        _mockUserServices.Setup(m => m.GetUserByEmail(_validEmail)).Returns(user);

        // Setup for nonexistent user
        _mockUserServices
            .Setup(m => m.GetUserByEmail(It.Is<string>(e => e != _validEmail)))
            .Throws<KeyNotFoundException>();

        _authServices = new AuthServices(_mockUserServices.Object);
    }

    [Fact]
    public void LogIn_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        AuthRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _authServices.LogIn(request));
    }

    [Fact]
    public void LogIn_MissingEmail_ThrowsArgumentException()
    {
        // Arrange
        AuthRequest request = new() { Email = null, Password = _validPassword };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _authServices.LogIn(request));
    }

    [Fact]
    public void LogIn_MissingPassword_ThrowsArgumentException()
    {
        // Arrange
        AuthRequest request = new() { Email = _validEmail, Password = null };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _authServices.LogIn(request));
    }

    [Fact]
    public void LogIn_InvalidEmail_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        AuthRequest request = new()
        {
            Email = "nonexistent@example.com",
            Password = _validPassword,
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _authServices.LogIn(request));
    }

    [Fact]
    public void LogIn_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        AuthRequest request = new() { Email = _validEmail, Password = "wrongpassword" };

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _authServices.LogIn(request));
    }

    [Fact]
    public void LogIn_ValidCredentials_ReturnsAuthResponse()
    {
        // Arrange
        AuthRequest request = new() { Email = _validEmail, Password = _validPassword };

        _mockUserServices
            .Setup(m => m.GetUserByEmail(_validEmail))
            .Returns(
                new User
                {
                    UserId = _userId,
                    Email = _validEmail,
                    Password = _validPassword,
                    Name = "Test User",
                }
            );

        // Act
        var response = _authServices.LogIn(request);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Token);
        Assert.NotNull(response.RefreshToken);
        Assert.NotNull(response.User);
        Assert.Equal(_validEmail, response.User.Email);
    }
}
