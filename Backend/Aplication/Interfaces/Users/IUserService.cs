using Aplication.DTOs.Users;

namespace Aplication.Interfaces.Users;

public interface IUserService
{
    Task<UserResponseDTO> CreateUserAsync(UserRequestDTO userRequest);
}