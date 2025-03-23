using Aplication.DTOs.Posts;

namespace Aplication.Interfaces.Posts;

public interface IPostService
{
    Task<IEnumerable<PostReponseDTO>> GetAllNearAsync(int currentLocationId);
}