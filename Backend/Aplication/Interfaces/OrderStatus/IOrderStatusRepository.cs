using Domain.Entities;

namespace Aplication.Interfaces;

public interface IOrderStatusRepository
{
    Task<IEnumerable<OrderStatus>> getAllASync();
    Task<OrderStatus?> getByIdASync(int id);
}