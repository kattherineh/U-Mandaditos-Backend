using Domain.Entities;

namespace Aplication.Interfaces {
    public interface IOrderStatusHistoryRepository {

        Task<IEnumerable<OrderStatusHistory>> GetAllAsync();
        Task<OrderStatusHistory?> GetByIdAsync(int id);
        Task AddAsync(OrderStatusHistory orderStatusHistory);
        Task<bool> UpdateAsync(OrderStatusHistory orderStatusHistory);
        Task<bool> DeleteAsync(int id);
        
    }
}