using Aplication.DTOs;
using Aplication.Interfaces;
using Domain.Entities;

namespace Aplication.Services;

public class OrderStatusService: IOrderStatusService
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    
    public OrderStatusService(IOrderStatusRepository orderStatusRepository)
    {
        _orderStatusRepository = orderStatusRepository;
    }
    
    public async Task<IEnumerable<OrderStatusResponseDTO>> getAllASync()
    {
        var orderStatusList = await _orderStatusRepository.getAllASync();
        return orderStatusList.Select(o=> new OrderStatusResponseDTO{Id = o.Id, Name = o.Name});
    }

    public async Task<OrderStatusResponseDTO?> getByIdASync(int id)
    {
        var orderStatus = await _orderStatusRepository.getByIdASync(id);
        return orderStatus is null ? null : new OrderStatusResponseDTO{Id = orderStatus.Id, Name = orderStatus.Name};
    }
}