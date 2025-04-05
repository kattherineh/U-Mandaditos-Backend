using Aplication.DTOs.Mandaditos;
using Aplication.Interfaces.Mandaditos;
using Domain.Entities;
using FirebaseAdmin.Auth.Hash;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MandaditoRepository : IMandaditoRepository
    {
        private readonly BackendDbContext _context;

        public MandaditoRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mandadito>> GetAllAsync()
        {
            return await _context.Mandaditos
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.LastLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.ProfilePic)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PickUpLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.DeliveryLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.LastLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.ProfilePic)
                .ToListAsync();
        }

        public async Task<Mandadito?> GetByIdAsync(int id)
        {
            var md = await _context.Mandaditos
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.LastLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PosterUser)
                        .ThenInclude(u => u.ProfilePic)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.PickUpLocation)
                .Include(m => m.Post)
                    .ThenInclude(p => p!.DeliveryLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.LastLocation)
                .Include(m => m.Offer)
                    .ThenInclude(o => o!.UserCreator)
                        .ThenInclude(u => u!.ProfilePic)
                .Include(m => m.Offer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (md is null) return null;

            /* match con ratings */
            var ratings = await _context.Ratings
                .Include(r => r.RatedUser)
                    .ThenInclude(u => u!.LastLocation)
                .Include(r => r.RatedUser)
                    .ThenInclude(u => u!.ProfilePic)
                .Where(r => r.IdMandadito == md.Id)
                .ToListAsync();

            md.Ratings = ratings;

            return md;
        }


        public async Task AddAsync(Mandadito mandadito)
        {
            _context.Mandaditos.Add(mandadito);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mandadito = await _context.Mandaditos.FindAsync(id);
            if (mandadito is null) return false;

            _context.Mandaditos.Remove(mandadito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Mandadito mandadito)
        {
            _context.Mandaditos.Update(mandadito);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}