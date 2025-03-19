using Domain.Entities;

namespace Aplication.Interfaces.SessionLogs;

public interface ISessionLogRepository
{
    Task<IEnumerable<SessionLog>> GetAllAsync();
    Task<SessionLog?> GetByIdAsync(int id);
    Task AddAsync(SessionLog sessionLog);
    Task<bool> UpdateAsync(SessionLog sessionLog);
    Task<bool> DeleteAsync(int id);
}