using Aplication.DTOs;

namespace Aplication.Interfaces {
    public interface IRatingService
    {
        Task<RatingResponseDTO> CreateAsync(RatingRequestDTO ratingRequestDTO);
        Task<bool> UpdateAsync(int id, RatingRequestDTO ratingRequestDTO);
        Task<bool> DeleteAsync(int id);
        Task<RatingResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<RatingResponseDTO?>> GetByRatedUserAsync(int idRatedUser);
/*         Task<IEnumerable<RatingResponseDTO?>> GetByMandaditoAsync(int idMandadito);
 */    }
}