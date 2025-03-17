using Aplication.DTOs.UserRole;
using Aplication.Interfaces;
namespace Aplication.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _orderUserRoleRepository;
     
    public UserRoleService(IUserRoleRepository orderUserRoleRepository)
    {
        _orderUserRoleRepository = orderUserRoleRepository;
    }

    public async Task<IEnumerable<UserRoleResponseDTO>> GetAllAsync()
    {
        var userRoleList = await _orderUserRoleRepository.GetAllAsync();
        return userRoleList.Select(o=> new UserRoleResponseDTO(){Id = o.Id, Name = o.Name});
    }

    public async Task<UserRoleResponseDTO?> getByIdAsync(int id)
    {
        var userRole = await _orderUserRoleRepository.GetByIdAsync(id);
        return userRole is null ? null : new UserRoleResponseDTO{Id = userRole.Id, Name = userRole.Name};
    }
}