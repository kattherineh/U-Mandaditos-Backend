using Aplication.Interfaces;
using Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderStatusHistoryRepository : IOrderStatusHistoryRepository
    {

        private readonly BackendDbContext _context;

        public OrderStatusHistoryRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatusHistory>> GetAllAsync()
        {
            return await _context.OrderStatusHistories.ToListAsync();
        }

        public async Task<OrderStatusHistory?> GetByIdAsync(int id)
        {
            return await _context.OrderStatusHistories.FindAsync(id);
        }

        public async Task AddAsync(OrderStatusHistory orderStatusHistory)
        {
            _context.OrderStatusHistories.Add(orderStatusHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(OrderStatusHistory orderStatusHistory)
        {
            _context.OrderStatusHistories.Update(orderStatusHistory);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderStatusHistory = await _context.OrderStatusHistories.FindAsync(id);
            if (orderStatusHistory is null) return false;

            _context.OrderStatusHistories.Remove(orderStatusHistory);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}