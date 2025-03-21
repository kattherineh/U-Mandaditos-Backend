using Aplication.Interfaces.Medias;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class MediaRepository : IMediaRepository
    {
        private readonly BackendDbContext _context;

        public MediaRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Media>> GetAllAsync()
        {
            return await _context.Media.ToListAsync();
        }

        public async Task<Media> GetByIdAsync(int id)
        {
            return await _context.Media.FindAsync(id);
        }

        public Task AddAsync(Media media)
        {
            _context.Media.Add(media);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Media media)
        {
            _context.Media.Update(media);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var media = await _context.Media.FindAsync(id);
            if (media is null) return false;

            _context.Media.Remove(media);

            return _context.SaveChanges() > 0;
        }

    }
}
