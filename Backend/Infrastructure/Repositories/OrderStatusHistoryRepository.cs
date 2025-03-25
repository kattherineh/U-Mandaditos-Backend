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
            return await _context.OrderStatusHistories
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => new { u.LastLocation, u.ProfilePic, u.Career })
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => new { p!.DeliveryLocation, p.PickUpLocation })
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Offer)
                        .ThenInclude(o => o!.UserCreator)
                            .ThenInclude(u => new { u!.Career, u.LastLocation, u.ProfilePic })
                .Include(o => o.OrderStatus)
                .ToListAsync();
        }

        public async Task<OrderStatusHistory?> GetByIdAsync(int id)
        {
            return await _context.OrderStatusHistories
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.LastLocation)
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.ProfilePic)
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => p!.PosterUser)
                            .ThenInclude(u => u.Career)
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Post)
                        .ThenInclude(p => new { p!.DeliveryLocation, p.PickUpLocation })
                .Include(o => o.Mandadito)
                    .ThenInclude(m => m!.Offer)
                        .ThenInclude(o => o!.UserCreator)
                            .ThenInclude(u => new { u!.Career, u.LastLocation, u.ProfilePic })
                .FirstOrDefaultAsync(o => o.Id == id);
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

        public async Task<IEnumerable<OrderStatusHistory>> GetAllByMandaditoAsync(int mandaditoId)
        {
            return await _context.OrderStatusHistories
                .Include(o => o.OrderStatus)
                    .ThenInclude(o => o!.Name)
                .Where(o => o.IdMandadito == mandaditoId)
                .ToListAsync();
        }

        public async Task<bool> UpdateActiveAsync(int id, bool active)
        {
            var existingOrderStatusHistory = await _context.OrderStatusHistories
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrderStatusHistory == null)
            {
                return false;
            }

            existingOrderStatusHistory.Active = active;
            _context.OrderStatusHistories.Update(existingOrderStatusHistory);
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