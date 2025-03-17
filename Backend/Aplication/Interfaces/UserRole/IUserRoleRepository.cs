namespace Aplication.Interfaces;

public interface IUserRoleRepository
{
    Task<IEnumerable<Domain.Entities.UserRole>> GetAllAsync();
    
    Task<Domain.Entities.UserRole> GetByIdAsync(int id);

}