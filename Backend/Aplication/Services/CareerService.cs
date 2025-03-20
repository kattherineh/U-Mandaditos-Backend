using Aplication.DTOs;
using Aplication.Interfaces;

namespace Aplication.Services;

public class CareerService: ICareerService
{
    private readonly ICareerRepository _careerRepository;
    
    public CareerService(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }
    
    public async Task<IEnumerable<CareerResponseDTO>> GetAllAsync()
    {
        var careerList = await _careerRepository.GetAllAsync();
        return careerList.Select(c=> new CareerResponseDTO{Id = c.Id, Name = c.Name});
    }

    public async Task<CareerResponseDTO?> GetByIdASync(int id)
    {
        var career = await _careerRepository.GetByIdAsync(id);
        return career is null ? null : new CareerResponseDTO{Id = career.Id, Name = career.Name};
    }
}