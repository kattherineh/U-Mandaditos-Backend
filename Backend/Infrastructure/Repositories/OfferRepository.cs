using Aplication.Interfaces.Offers;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public  class OfferRepository: IOfferRepository
    {
        private readonly BackendDbContext _context;

        public OfferRepository(BackendDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers.FindAsync(id);
        }

        public async Task AddAsync(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer is null) return false;

            _context.Offers.Remove(offer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            _context.Offers.Update(offer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
