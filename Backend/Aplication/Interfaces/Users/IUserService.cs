using Aplication.DTOs.General;
using System.Threading.Tasks;
using Aplication.DTOs.Users;
using Azure;

namespace Aplication.Interfaces.Users;

public interface IUserService
{
    Task<ResponseDTO<UserResponseDTO>> CreateUserAsync(UserRequestDTO userRequest);
    Task<UserResponseDTO?> GetByEmailAsync(string email);
    Task<ResponseDTO<UserProfileResponseDTO>> GetByIdAsync(int id);
    Task<ResponseDTO<UpdatedResponseDTO>> UpdateAsync(int id, UserProfileRequestDTO user);
    Task<ResponseDTO<bool>> ChangePasswordAsync(int id, string password);
    Task<ResponseDTO<UserProfileResponseDTO>> GetUser();
}