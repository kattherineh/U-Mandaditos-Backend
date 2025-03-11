using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class OrderStatusRepository: IOrderStatusRepository 
{
    private readonly BackendDbContext _context;
    
    public OrderStatusRepository(BackendDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderStatus>> getAllASync()
    {
        return await _context.OrderStatus.ToListAsync();
    }

    public async Task<OrderStatus?> getByIdASync(int id)
    {
        return await _context.OrderStatus.FindAsync(id);
    }
}