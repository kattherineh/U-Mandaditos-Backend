using Aplication.DTOs.Auth.Login;
using Aplication.DTOs.General;
using Aplication.DTOs.Locations;

namespace Aplication.Interfaces.Locations
{
    public interface ILocationService
    {
        Task<ResponseDTO<IEnumerable<LocationResponseDTO>>> GetAllAsync();
        Task<ResponseDTO<LocationResponseDTO>> GetByIdAsync(int id);
    }
}
