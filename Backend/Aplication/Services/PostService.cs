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

    public async Task<IEnumerable<PostResponseDTO>> GetAllNearAsync(int currentLocationId)
    {
        var posts = await _postRepository.GetAllAsync();
        return posts.Where(post => post.IdPickUpLocation == currentLocationId).Select(post => new PostResponseDTO
        {
            Id = post.Id,
            Description = post.Description,
            SuggestedValue = post.SugestedValue,
            PosterUserName = post.PosterUser.Name,
            CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm")
        });
    }
    
    public async Task<IEnumerable<PostResponseDTO>> GetPostsNearLocationAsync(int idLocation)
    {
        var posts = await _postRepository.GetPostsNearLocationAsync(idLocation, 70);

        var postDtos = posts.Select(p => new PostResponseDTO
        {
            Id = p.Id,
            Description = p.Description,
            PickUpLocation = p.PickUpLocation?.Name ?? "Ubicación no disponible",
            DeliveryLocation = p.DeliveryLocation?.Name ?? "Ubicación no disponible",
            CreatedAt = p.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
            PosterUserName = p.PosterUser?.Name ?? "Usuario desconocido"
        }).ToList();

        return postDtos;
    }

    public async Task<IEnumerable<PostResponseDTO>> GetPostsByPosterUserIdAsync(int idPosterUser)
    {
        var posts = await _postRepository.GetPostsByPosterUserId(idPosterUser);

        var postDtos = posts.Select(p => new PostResponseDTO
        {
            Id = p.Id,
            Description = p.Description,
            SuggestedValue = p.SugestedValue,
            PosterUserName = p.PosterUser?.Name ?? "Usuario desconocido",
            CreatedAt = p.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
            PickUpLocation = p.PickUpLocation?.Name ?? "Ubicación no disponible",
            DeliveryLocation = p.DeliveryLocation?.Name ?? "Ubicación no disponible"
        }).ToList();

        return postDtos;
    }

    public async Task<int> GetPostsCountAsync(int idPosterUser)
    {
        var posts = await _postRepository.GetPostsByPosterUserId(idPosterUser);
        return posts.Count();
    }

    public async Task<PostResponseDTO> GetPostByIdAsync(int idPost)
    {
        var post =  await _postRepository.GetByIdAsync(idPost);
        if (post == null)
        {
            return null;
        }

        return new PostResponseDTO()
        {
            Id = post.Id,
            Description = post.Description,
            CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
            PosterUserName = post.PosterUser?.Name ?? "Usuario desconocido",
            DeliveryLocation = post.DeliveryLocation?.Name ?? "Ubicación no disponible",
            PickUpLocation = post.PickUpLocation?.Name ?? "Ubicación no disponible",
            SuggestedValue = post.SugestedValue
        };
    }
}