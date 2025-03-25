using Domain.Entities;

namespace Aplication.Interfaces {
    public interface IOrderStatusHistoryRepository {

        Task<IEnumerable<OrderStatusHistory>> GetAllByMandaditoAsync(int idMandadito);
        Task<OrderStatusHistory?> GetByIdAsync(int id);
        Task AddAsync(OrderStatusHistory orderStatusHistory);
        Task<bool> UpdateActiveAsync(int id, bool isActive);
        Task<bool> DeleteAsync(int id);
        
    }
}