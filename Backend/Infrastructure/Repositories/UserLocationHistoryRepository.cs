using Aplication.Interfaces.UserLocationHistory;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserLocationHistoryRepository: IUserLocationHistoryRepository
{
    private BackendDbContext _context;
    
    public UserLocationHistoryRepository(BackendDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UserLocationHistory>> GetAllAsync()
    {
        return await _context.UserLocationHistories.ToListAsync();
    }

    public async Task<UserLocationHistory?> GetByIdAsync(int id)
    {
        return await _context.UserLocationHistories.FindAsync(id);
    }

    public async Task AddAsync(UserLocationHistory post)
    {
        _context.UserLocationHistories.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(UserLocationHistory userLocationHistory)
    {
        _context.UserLocationHistories.Update(userLocationHistory);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userLocationHistory = await _context.UserLocationHistories.FindAsync(id);
        if (userLocationHistory is null) return false;
        _context.UserLocationHistories.Update(userLocationHistory);
        return await _context.SaveChangesAsync() > 0;
    }
}