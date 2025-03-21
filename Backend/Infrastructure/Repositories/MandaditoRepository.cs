using Aplication.Interfaces.Mandaditos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MandaditoRepository: IMandaditoRepository
    {
        private readonly BackendDbContext _context;

        public MandaditoRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mandadito>> GetAllAsync()
        {
            return await _context.Mandaditos.ToListAsync();
        }

        public async Task<Mandadito?> GetByIdAsync(int id)
        {
            return await _context.Mandaditos.FindAsync(id);
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
