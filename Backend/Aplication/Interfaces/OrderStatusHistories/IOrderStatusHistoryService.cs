using Aplication.DTOs;

namespace Aplication.Interfaces {
    public interface IOrderStatusHistoryService
    {
        Task<IEnumerable<OrderStatusHistoryResponseDTO>> GetAllByMandaditoAsync(int idMandadito);
        Task<OrderStatusHistoryResponseDTO?> GetByIdAsync(int id);
        Task<OrderStatusHistoryResponseDTO> AddAsync(OrderStatusHistoryRequestDTO dto);
        Task<bool> UpdateActiveAsync(int id, bool isActive);
        Task<bool> DeleteAsync(int id);
    }
}