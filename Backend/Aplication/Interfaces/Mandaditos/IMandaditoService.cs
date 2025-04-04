using Aplication.DTOs.General;
using Aplication.DTOs.Mandaditos;

namespace Aplication.Interfaces.Mandaditos;

public interface IMandaditoService
{
    Task<ResponseDTO<MandaditoResponseDTO?>> GetByIdAsync(int id);
    Task<IEnumerable<MandaditoHistoryResponseDTO>?> GetHistoryAsync(int userId);
    Task<MandaditoResponseMinDTO?> CreateAsync(MandaditoRequestDTO dto);
}