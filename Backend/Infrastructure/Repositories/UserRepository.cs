using Aplication.Interfaces.Users;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private BackendDbContext _dbContext;

    public UserRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users
                .Include(u => u.ProfilePic)
                .Include(u => u.Career)
                .Include(u => u.LastLocation)
                .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return foundUser;
    }

    public async Task AddAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null) return false;
        _dbContext.Users.Remove(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangePasswordAsync(int id, string password)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null) return false;
        user.Password = password;
        return await _dbContext.SaveChangesAsync() > 0;
    }
}