using Aplication.DTOs.General;
using Aplication.DTOs.Posts;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Locations;
using Aplication.Interfaces.Posts;
using Aplication.Interfaces.Users;
using Domain.Entities;

namespace Aplication.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ILocationRepository locationRepository, IAuthenticatedUserService authenticatedUserService)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _locationRepository = locationRepository;
        _authenticatedUserService = authenticatedUserService;
    }
    
    public async Task<ResponseDTO<PostResponseDTO>> CreateAsync(PostRequestDTO dto)
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId(); //Obtiene el id del usuario autenticado
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "El usuario no existe.",
                Data = null
            };
        }
        
        var pickUpLocation = await _locationRepository.GetByIdAsync(dto.IdPickUpLocation);
        if (pickUpLocation == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "La ubicación de recogida no existe.",
                Data = null
            };
        }
        
        var deliveryLocation = await _locationRepository.GetByIdAsync(dto.IdDeliveryLocation);
        if (deliveryLocation == null)
        {
            return new ResponseDTO<PostResponseDTO>
            {
                Success = false,
                Message = "La ubicación de entrega no existe.",
                Data = null
            };
        }
        
        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description,
            SugestedValue = dto.SuggestedValue,
            IdPickUpLocation = dto.IdPickUpLocation,
            IdDeliveryLocation = dto.IdDeliveryLocation,
            IdPosterUser = userId,
            CreatedAt = DateTime.Now,
            Completed = false //Por defecto el post no esta completado
        };

        await _postRepository.AddAsync(post);
        
        return new ResponseDTO<PostResponseDTO>
        {
            Success = true,
            Message = "Se ha creado un nuevo post correctamente.",
            Data = new PostResponseDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                SuggestedValue = post.SugestedValue,
                PosterUserName = post.PosterUser != null ? post.PosterUser.Name : "Usuario desconocido",
                PickUpLocation = pickUpLocation.Name,
                DeliveryLocation = deliveryLocation.Name,
                CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm")
            }
        };
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