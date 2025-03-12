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

        public async Task<IEnumerable<LocationResponseDTO>> GetAllAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return locations.Select(l => new LocationResponseDTO { Id = l.Id, Name = l.Name, Description = l.Description, Latitude = l.Latitude, Longitude = l.Longitude, Active = l.Active });
        }

        public async Task<LocationResponseDTO> GetByIdAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            return location is null ? null : new LocationResponseDTO { Id = location.Id, Name = location.Name, Description = location.Description, Latitude = location.Latitude, Longitude = location.Longitude, Active = location.Active };
        }
    }
}
