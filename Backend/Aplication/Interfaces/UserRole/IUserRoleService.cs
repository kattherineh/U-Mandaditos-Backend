using Aplication.DTOs.UserRole;

namespace Aplication.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRoleResponseDTO>> GetAllAsync();
    
    Task<UserRoleResponseDTO?> getByIdAsync(int id);
}