using Aplication.DTOs.General;
using System.Threading.Tasks;
using Aplication.DTOs.Users;

namespace Aplication.Interfaces.Users;

public interface IUserService
{
    Task<ResponseDTO<UserResponseDTO>> CreateUserAsync(UserRequestDTO userRequest);
    Task<ResponseDTO<UserProfileResponseDTO>> GetByIdAsync(int id);
    Task<ResponseDTO<UpdatedResponseDTO>> UpdateAsync(int id, UserProfileRequestDTO user);
}