using Aplication.Interfaces.SessionLogs;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SessionLogRepository : ISessionLogRepository
{
    
    private BackendDbContext _context;
    
    public SessionLogRepository(BackendDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<SessionLog>> GetAllAsync()
    {
        return await _context.SessionLogs.ToListAsync();
    }

    public async Task<SessionLog?> GetByIdAsync(int id)
    {
        return await _context.SessionLogs.FindAsync(id);
    }

    public async Task AddAsync(SessionLog sessionLog)
    {
        _context.SessionLogs.Add(sessionLog);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(SessionLog sessionLog)
    {
        _context.SessionLogs.Update(sessionLog);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sessionLog = await _context.SessionLogs.FindAsync(id);
        if (sessionLog is null) return false;
        _context.SessionLogs.Update(sessionLog);
        return await _context.SaveChangesAsync() > 0;
    }
}