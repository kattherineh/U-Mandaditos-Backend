using Domain.Entities;

namespace Aplication.Interfaces;

public interface ICareerRepository
{
    Task<IEnumerable<Career>> GetAllAsync();
    Task<Career?> GetByIdAsync(int id);
}
