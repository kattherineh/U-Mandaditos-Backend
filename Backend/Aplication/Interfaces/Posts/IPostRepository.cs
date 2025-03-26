using Domain.Entities;

namespace Aplication.Interfaces.Posts;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(int id);
    Task AddAsync(Post post);
    Task<bool> UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Post>> GetPostsNearLocationAsync(int idLocation, double radiusMeters);
    Task<IEnumerable<Post>> GetPostsByPosterUserId(int idPosterUser);
}