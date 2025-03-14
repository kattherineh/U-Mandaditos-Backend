using Aplication.DTOs.Media;
using Aplication.Interfaces.Medias;
using Domain.Entities;

namespace Aplication.Services
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<IEnumerable<MediaResponseDTO>> GetAllAsync()
        {
            var media = await _mediaRepository.GetAllAsync();
            return media.Select(m => new MediaResponseDTO { Id = m.Id, Name = m.Name, Link = m.Link });
        }

        public async Task<MediaResponseDTO> GetByIdAsync(int id)
        {
            var media = await _mediaRepository.GetByIdAsync(id);
            return media is null ? null : new MediaResponseDTO { Id = media.Id, Name = media.Name, Link = media.Link };
        }

        public async Task<MediaResponseDTO> CreateAsync(MediaRequestDTO dto)
        {
            var media = new Media(dto.Name, dto.Link);
            await _mediaRepository.AddAsync(media);
            return new MediaResponseDTO { Id = media.Id, Name = media.Name, Link = media.Link };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _mediaRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, MediaRequestDTO dto)
        {
            var media = await _mediaRepository.GetByIdAsync(id);

            if (media is null)
                return false;

            media.Name = dto.Name;
            media.Link = dto.Link;

            return await _mediaRepository.UpdateAsync(media);
        }
    }
}