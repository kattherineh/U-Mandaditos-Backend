using Aplication.DTOs.General;
using Aplication.DTOs.Posts;

namespace Aplication.Interfaces.Posts;

public interface IPostService
{
    
    Task<ResponseDTO<PostResponseDTO>> CreateAsync(PostRequestDTO dto);
    Task<IEnumerable<PostResponseDTO>> GetAllNearAsync(int currentLocationId);
    Task<IEnumerable<PostResponseDTO>> GetPostByLocation(int currentLocationId);
    Task<IEnumerable<PostResponseDTO>> GetPostsByPosterUserIdAsync(int idPosterUser);
    Task<int> GetPostsCountAsync(int idPosterUser);
    Task<PostResponseDTO> GetPostByIdAsync(int idPost);
    Task<IEnumerable<PostResponseDTO>> GetActivePosts();

}