using Aplication.DTOs.Posts;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Posts;

namespace Aplication.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostReponseDTO>> GetAllNearAsync(int currentLocationId)
    {
        var posts = await _postRepository.GetAllAsync();
        return posts.Where(post => post.IdPickUpLocation == currentLocationId).Select(post => new PostReponseDTO
        {
            Id = post.Id,
            Description = post.Description,
            SuggestedValue = post.SugestedValue,
            PosterUserName = post.PosterUser.Name,
            CreatedAt = post.CreatedAt
        });
    }
}