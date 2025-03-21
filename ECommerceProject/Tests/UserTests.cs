using ServiceContracts;
using ServiceContracts.DTO.UserDto;
using Services;

namespace Tests;

public class UserTests
{
    private readonly IUserServices _userServices = new UserServices();

    [Fact]
    public void AddUser_NullUser_ThrowsArgumentNullException()
    {
        // Arrange
        UserAddRequest? request = null;

        // Act
        void action() => _userServices.AddUser(request);

        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void AddUser_ValidUser_ReturnsUserResponse()
    {
        // Arrange
        UserAddRequest request = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password",
        };

        // Act
        UserResponse response = _userServices.AddUser(request);
        UserResponse? userResponse = _userServices.GetUserById(response.UserId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(response, userResponse);
    }

    [Fact]
    public void AddUser_InvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        UserAddRequest request = new()
        {
            Name = "John Doe",
            Email = "invalid-email", // Invalid email format
            Password = "password",
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userServices.AddUser(request));
    }

    [Fact]
    public void AddUser_ShortPassword_ThrowsArgumentException()
    {
        // Arrange
        UserAddRequest request = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "pass", // Too short
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userServices.AddUser(request));
    }

    [Fact]
    public void GetUserById_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? userId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userServices.GetUserById(userId));
    }

    [Fact]
    public void GetUserById_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _userServices.GetUserById(userId));
    }

    [Fact]
    public void GetAllUsers_NoUsers_ReturnsEmptyCollection()
    {
        // Act
        var paginatedResponse = _userServices.GetAllUsers();

        // Assert
        Assert.Empty(paginatedResponse.Items);
        Assert.Equal(0, paginatedResponse.TotalItems);
    }

    [Fact]
    public void GetAllUsers_WithUsers_ReturnsPaginatedUsers()
    {
        // Arrange
        UserAddRequest request1 = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password",
        };
        UserAddRequest request2 = new()
        {
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            Password = "password",
        };
        _userServices.AddUser(request1);
        _userServices.AddUser(request2);

        // Act
        var paginatedResponse = _userServices.GetAllUsers();

        // Assert
        Assert.Equal(2, paginatedResponse.Items.Count());
        Assert.Equal(2, paginatedResponse.TotalItems);
        Assert.Equal(1, paginatedResponse.PageNumber);
        Assert.Equal(10, paginatedResponse.PageSize);
    }

    [Fact]
    public void UpdateUser_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        UserUpdateRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userServices.UpdateUser(request));
    }

    [Fact]
    public void UpdateUser_NonExistingUser_ThrowsKeyNotFoundException()
    {
        // Arrange
        UserUpdateRequest request = new()
        {
            UserId = Guid.NewGuid(),
            Name = "Updated Name",
            Email = "updated.email@example.com",
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _userServices.UpdateUser(request));
    }

    [Fact]
    public void UpdateUser_ValidRequest_ReturnsUpdatedUser()
    {
        // Arrange
        UserAddRequest addRequest = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password",
        };
        UserResponse userResponse = _userServices.AddUser(addRequest);

        UserUpdateRequest updateRequest = new()
        {
            UserId = userResponse.UserId,
            Name = "John Updated",
            Email = "john.updated@example.com",
        };

        // Act
        UserResponse updatedResponse = _userServices.UpdateUser(updateRequest);

        // Assert
        Assert.Equal(updateRequest.UserId, updatedResponse.UserId);
        Assert.Equal(updateRequest.Name, updatedResponse.Name);
        Assert.Equal(updateRequest.Email, updatedResponse.Email);
    }

    [Fact]
    public void DeleteUser_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? userId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userServices.DeleteUser(userId));
    }

    [Fact]
    public void DeleteUser_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _userServices.DeleteUser(userId));
    }

    [Fact]
    public void DeleteUser_ExistingId_ReturnsTrue()
    {
        // Arrange
        UserAddRequest addRequest = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password",
        };
        UserResponse userResponse = _userServices.AddUser(addRequest);

        // Act
        bool result = _userServices.DeleteUser(userResponse.UserId);

        // Assert
        Assert.True(result);
        Assert.Throws<KeyNotFoundException>(() => _userServices.GetUserById(userResponse.UserId));
    }

    [Fact]
    public void GetUserByEmail_NullEmail_ThrowsArgumentNullException()
    {
        // Arrange
        string? email = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userServices.GetUserByEmail(email));
    }

    [Fact]
    public void GetUserByEmail_NonExistingEmail_ThrowsKeyNotFoundException()
    {
        // Arrange
        string email = "nonexisting@example.com";

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _userServices.GetUserByEmail(email));
    }

    [Fact]
    public void GetUserByEmail_ExistingEmail_ReturnsUser()
    {
        // Arrange
        UserAddRequest addRequest = new()
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password",
        };
        _userServices.AddUser(addRequest);

        // Act
        var user = _userServices.GetUserByEmail(addRequest.Email);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(addRequest.Email, user.Email);
        Assert.Equal(addRequest.Name, user.Name);
    }
}
