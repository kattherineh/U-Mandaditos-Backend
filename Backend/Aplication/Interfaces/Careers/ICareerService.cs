using Aplication.DTOs;

namespace Aplication.Interfaces;

public interface ICareerService
{
    Task<IEnumerable<CareerResponseDTO>> GetAllAsync();
    Task<CareerResponseDTO?> GetByIdASync(int id);
}