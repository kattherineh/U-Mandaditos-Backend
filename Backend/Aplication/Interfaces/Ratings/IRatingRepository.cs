using Domain.Entities;

namespace Aplication.Interfaces {

    public interface IRatingRepository {

        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating?> GetByIdAsync(int id);
        Task AddAsync(Rating rating);
        Task<bool> UpdateAsync(Rating rating);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Rating>> GetByRatedUserAsync(int idRatedUser);
        Task<IEnumerable<Rating>> GetByMandaditoAsync(int idMandadito);
    }
}