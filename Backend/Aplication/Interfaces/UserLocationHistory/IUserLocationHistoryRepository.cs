using Domain.Entities;

namespace Aplication.Interfaces.UserLocationHistory;

public interface IUserLocationHistoryRepository
{
    Task<IEnumerable<Domain.Entities.UserLocationHistory>> GetAllAsync();
    Task<Domain.Entities.UserLocationHistory?> GetByIdAsync(int id);
    Task AddAsync(Domain.Entities.UserLocationHistory userLocationHistory);
    Task<bool> UpdateAsync(Domain.Entities.UserLocationHistory userLocationHistory);
    Task<bool> DeleteAsync(int id);
}