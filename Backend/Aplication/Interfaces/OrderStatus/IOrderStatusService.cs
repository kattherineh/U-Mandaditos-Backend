using Aplication.DTOs;

namespace Aplication.Interfaces;

public interface IOrderStatusService
{
    Task<IEnumerable<OrderStatusResponseDTO>> getAllASync();
    Task<OrderStatusResponseDTO?> getByIdASync(int id);
}