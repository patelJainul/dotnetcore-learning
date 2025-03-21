using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.UserDto;
using Services.Helpers;

namespace Services;

public class UserServices : IUserServices
{
    private readonly List<User> _users = [];

    public UserResponse AddUser(UserAddRequest? userAddRequest)
    {
        ArgumentNullException.ThrowIfNull(userAddRequest);

        ValidationHelper.ModelValidation(userAddRequest);

        User user = userAddRequest.ToUser();
        _users.Add(user);

        return user.ToUserResponse();
    }

    public PaginatedResponse<UserResponse> GetAllUsers(int? pageNumber = 1, int? pageSize = 10)
    {
        return PaginatedResponseHelper.CreatePaginatedResponse(
            _users
                .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10))
                .Take(pageSize ?? 10)
                .Select(user => user.ToUserResponse()),
            _users.Count,
            pageNumber ?? 1,
            pageSize ?? 10
        );
    }

    public UserResponse GetUserById(Guid? userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        User? user =
            _users.FirstOrDefault(user => user.UserId == userId)
            ?? throw new KeyNotFoundException($"User with id {userId} not found");
        return user.ToUserResponse();
    }

    public UserResponse UpdateUser(UserUpdateRequest? userUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(userUpdateRequest);

        ValidationHelper.ModelValidation(userUpdateRequest);

        User? user =
            _users.FirstOrDefault(user => user.UserId == userUpdateRequest.UserId)
            ?? throw new KeyNotFoundException($"User with id {userUpdateRequest.UserId} not found");

        User updatedUser = userUpdateRequest.ToUpdatedUser(user);
        _users.Remove(user);
        _users.Add(updatedUser);

        return updatedUser.ToUserResponse();
    }

    public bool DeleteUser(Guid? userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        User? user =
            _users.FirstOrDefault(user => user.UserId == userId)
            ?? throw new KeyNotFoundException($"User with id {userId} not found");

        return _users.Remove(user);
    }

    public User GetUserByEmail(string? email)
    {
        ArgumentNullException.ThrowIfNull(email);

        User? user =
            _users.FirstOrDefault(user => user.Email == email)
            ?? throw new KeyNotFoundException($"User with email {email} not found");

        return user;
    }
}
