using Aplication.DTOs.Locations;

namespace Aplication.Interfaces.Locations
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationResponseDTO>> GetAllAsync();
        Task<LocationResponseDTO> GetByIdAsync(int id);
    }
}
