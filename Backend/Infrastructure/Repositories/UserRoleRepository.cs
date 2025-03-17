using Aplication.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly BackendDbContext _context;
    
    public UserRoleRepository(BackendDbContext context)
    {
        _context = context;
    }
    

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRole.ToListAsync();
    }

    public async Task<UserRole> GetByIdAsync(int id)
    {
        return await _context.UserRole.FindAsync(id);
    }
}