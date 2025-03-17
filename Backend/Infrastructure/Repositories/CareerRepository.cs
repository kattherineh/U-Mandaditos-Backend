using Aplication.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CareerRepository: ICareerRepository 
{
    private readonly BackendDbContext _context;
    
    public CareerRepository(BackendDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Career>> GetAllAsync()
    {
        return await _context.Careers.ToListAsync();
    }
    
    public async Task<Career?> GetByIdAsync(int id)
    {
        return await _context.Careers.FindAsync(id);
    }
}