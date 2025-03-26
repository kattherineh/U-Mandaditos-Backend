using Aplication.Interfaces;
using Aplication.DTOs;
using Domain.Entities;

namespace Aplication.Services
{
    public class OrderStatusHistoryService : IOrderStatusHistoryService
    {
        private readonly IOrderStatusHistoryRepository _orderStatusHistoryRepository;

        public OrderStatusHistoryService(IOrderStatusHistoryRepository orderStatusHistoryRepository)
        {
            _orderStatusHistoryRepository = orderStatusHistoryRepository;
        }

        public async Task<IEnumerable<OrderStatusHistoryResponseDTO>> GetAllByMandaditoAsync(int idMandadito)
        {
            var osh = await _orderStatusHistoryRepository.GetAllByMandaditoAsync(idMandadito);
            return osh.Select(os => new OrderStatusHistoryResponseDTO
            {
                Id = os.Id,
                IdMandadito = os.IdMandadito,
                status = os.OrderStatus?.Name ?? "Unknown",
                Active = os.Active
            });
        }

        public async Task<OrderStatusHistoryResponseDTO?> GetByIdAsync(int id)
        {
            var osh = await _orderStatusHistoryRepository.GetByIdAsync(id);

            if (osh == null) return null;

            return new OrderStatusHistoryResponseDTO
            {
                Id = osh.Id,
                IdMandadito = osh.IdMandadito,
                status = osh.OrderStatus?.Name ?? "Unknown",
                Active = osh.Active
            };
        }

        public async Task<OrderStatusHistoryResponseDTO> AddAsync(OrderStatusHistoryRequestDTO request)
        {
            var osh = new OrderStatusHistory
            {
                IdMandadito = request.IdMandadito,
                IdStatus = request.IdStatus,
                Active = true,
                ChangeAt = DateTime.Now
            };

            await _orderStatusHistoryRepository.AddAsync(osh);

            return new OrderStatusHistoryResponseDTO
            {
                Id = osh.Id,
                IdMandadito = osh.IdMandadito,
                status = osh.OrderStatus?.Name ?? "Unknown",
                Active = osh.Active
            };
        }

        public async Task<bool> UpdateActiveAsync(int id, bool isActive)
        {
            return await _orderStatusHistoryRepository.UpdateActiveAsync(id, isActive);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _orderStatusHistoryRepository.DeleteAsync(id);
        }
    }
}