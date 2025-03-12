using Domain.Entities;

namespace Aplication.Interfaces.Locations
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
    }
}
