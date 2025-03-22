using Aplication.DTOs.Posts;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Posts;

namespace Aplication.Services;

public class PostService : IPostService
{
    
    private readonly IPostRepository _postRepository;
    private readonly IGeolocationService _geolocationService;
    
    public PostService(IPostRepository postRepository, IGeolocationService geolocationService)
    {
        _postRepository = postRepository;
        _geolocationService = geolocationService;
    }
    
    public async Task<IEnumerable<PostReponseDTO>> GetAllNearAsync(double lat, double lon)
    {
        var posts = await _postRepository.GetAllAsync();
        return posts.Where(post => _geolocationService.CalculateDistance(post.PickUpLocation.Latitude, post.PickUpLocation.Longitude,lat, lon) <= 5)
            .Select(post => new PostReponseDTO
            {
                Id = post.Id,
                Description = post.Description,
                SuggestedValue = post.SugestedValue,
                PosterUserName = post.PosterUser.Name,
                CreatedAt = post.CreatedAt
            });
    }
}