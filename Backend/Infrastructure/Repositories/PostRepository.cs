using Aplication.Interfaces.Posts;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository: IPostRepository
{
    private BackendDbContext _context;
    
    public PostRepository(BackendDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task AddAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post is null) return false;
        _context.Posts.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }
}