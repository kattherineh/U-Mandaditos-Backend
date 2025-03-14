using Aplication.DTOs.Media;

namespace Aplication.Interfaces.Medias
{
    public interface IMediaService
    {
        Task<IEnumerable<MediaResponseDTO>> GetAllAsync();
        Task<MediaResponseDTO> GetByIdAsync(int id);
        Task<MediaResponseDTO> CreateAsync(MediaRequestDTO dto);
        Task<bool> UpdateAsync(int id, MediaRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
