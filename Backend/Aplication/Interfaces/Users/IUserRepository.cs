using Domain.Entities;

namespace Aplication.Interfaces.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<bool> ChangePasswordAsync(int id, string password);
    Task<User?> GetByEmailAsync(string email);
}