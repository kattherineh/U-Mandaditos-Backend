using Aplication.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly BackendDbContext _context;

        public RatingRepository(BackendDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.LastLocation)
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.ProfilePic)
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.Career)
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => new { p!.DeliveryLocation, p.PickUpLocation })
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Offer)
                        .ThenInclude(o => o!.UserCreator)
                            .ThenInclude(u => new { u!.Career, u.LastLocation, u.ProfilePic })
                .Include(r => r.RaterUser)
                    .ThenInclude(u => new { u!.LastLocation, u.ProfilePic, u.Career })
                .Include(r => r.RatedUser)
                    .ThenInclude(u => new { u!.LastLocation, u.ProfilePic, u.Career })
                .Include(r => r.RatedRole)
                .ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _context.Ratings
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => new { u!.LastLocation, u.ProfilePic, u.Career })
                .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Post)
                                .ThenInclude(p => p!.DeliveryLocation)
                        .Include(r => r.Mandadito)
                            .ThenInclude(m => m!.Post)
                                .ThenInclude(p => p!.PickUpLocation)
                        .Include(r => r.Mandadito)
                    .ThenInclude(m => m!.Offer)
                        .ThenInclude(o => o!.UserCreator)
                            .ThenInclude(u => new { u!.Career, u.LastLocation, u.ProfilePic })
                .Include(r => r.RaterUser)
                    .ThenInclude(u => new { u!.LastLocation, u.ProfilePic, u.Career })
                .Include(r => r.RatedUser)
                    .ThenInclude(u => new { u!.LastLocation, u.ProfilePic, u.Career })
                .Include(r => r.RatedRole)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating is null) return false;

            _context.Ratings.Remove(rating);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}