using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly BackendDbContext _context;

        public MessageRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages
                .Include(msg => msg.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(pu => new { pu.LastLocation, pu.Career, pu.ProfilePic })
                .Include(msg => msg.User)
                    .ThenInclude(u => new { u!.ProfilePic, u.LastLocation, u.Career })
                .ToListAsync();
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Include(msg => msg.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(pu => new { pu.LastLocation, pu.Career, pu.ProfilePic })
                .Include(msg => msg.User)
                    .ThenInclude(u => new { u!.ProfilePic, u.LastLocation, u.Career })
                .FirstOrDefaultAsync(msg => msg.Id == id);

        }

        public async Task AddAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message is null) return false;

            _context.Messages.Remove(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Message message)
        {
            _context.Messages.Update(message);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}