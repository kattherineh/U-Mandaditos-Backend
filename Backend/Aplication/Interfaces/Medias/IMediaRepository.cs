using Domain.Entities;

namespace Aplication.Interfaces.Medias
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAllAsync();
        Task<Media?> GetByIdAsync(int id);
        Task AddAsync(Media media);
        Task<bool> UpdateAsync(Media media);
        Task<bool> DeleteAsync(int id);
    }
}
