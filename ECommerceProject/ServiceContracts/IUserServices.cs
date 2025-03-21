using Entities;
using ServiceContracts.DTO;
using ServiceContracts.DTO.UserDto;

namespace ServiceContracts;

public interface IUserServices
{
    public UserResponse AddUser(UserAddRequest? userAddRequest);

    public PaginatedResponse<UserResponse> GetAllUsers(int? pageNumber = 1, int? pageSize = 10);

    public UserResponse GetUserById(Guid? userId);

    public User GetUserByEmail(string? email);

    public UserResponse UpdateUser(UserUpdateRequest? userUpdateRequest);

    public bool DeleteUser(Guid? userId);
}
