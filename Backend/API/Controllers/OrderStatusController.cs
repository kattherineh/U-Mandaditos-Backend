using Aplication.DTOs;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/orderStatus")]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService;

    public OrderStatusController(IOrderStatusService orderStatusService)
    {
        _orderStatusService = orderStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _orderStatusService.getAllASync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orderStatus = await _orderStatusService.getByIdASync(id);
        return orderStatus is null ? NotFound() : Ok(orderStatus);
    }
}