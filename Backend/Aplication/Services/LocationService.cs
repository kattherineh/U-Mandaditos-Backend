using Aplication.DTOs.General;
using Aplication.DTOs.Locations;
using Aplication.Interfaces.Locations;

namespace Aplication.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<ResponseDTO<IEnumerable<LocationResponseDTO>>> GetAllAsync()
        {
            try
            {
                var locations = await _locationRepository.GetAllAsync();

                return new ResponseDTO<IEnumerable<LocationResponseDTO>>
                {
                    Success = true,
                    Message = "Ubicaciones obtenidas correctamente",
                    Data = locations.Select(l => new LocationResponseDTO { Id = l.Id, Name = l.Name, Description = l.Description, Latitude = l.Latitude, Longitude = l.Longitude, Active = l.Active })
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDTO<IEnumerable<LocationResponseDTO>>
                {
                    Success = false,
                    Message = "Ocurrió un error al obtener las ubicaciones",
                };
            } 
        }

        public async Task<ResponseDTO<LocationResponseDTO>> GetByIdAsync(int id)
        {
            try
            {
                var location = await _locationRepository.GetByIdAsync(id);
                return new ResponseDTO<LocationResponseDTO>
                {
                    Success = true,
                    Message = $"La ubicación con id={id} fué obtenida satisfactoriamente",
                    Data = location is null ? null : new LocationResponseDTO { Id = location.Id, Name = location.Name, Description = location.Description, Latitude = location.Latitude, Longitude = location.Longitude, Active = location.Active }
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return new ResponseDTO<LocationResponseDTO>
                {
                    Success = true,
                    Message = $"Ocurrió un error al obtener la ubicación con id={id}"
                };
            }
            
        }
    }
}
