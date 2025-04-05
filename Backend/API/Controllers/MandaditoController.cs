using Aplication.DTOs.Mandaditos;
using Aplication.Interfaces.Mandaditos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/mandadito")]
public class MandaditoController : ControllerBase
{
    private readonly IMandaditoService _mandaditoService;
    
    public MandaditoController(IMandaditoService mandaditoService)
    {
        _mandaditoService = mandaditoService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetMandaditoById(int id)
    {
        var mandadito = await _mandaditoService.GetByIdAsync(id);
        return mandadito is null ? NotFound($"El mandadito con id {id} no existe.") : Ok(mandadito);
    }
    
    [HttpGet("history/get")]
    public async Task<IActionResult> GetHistory([FromQuery] int userId)
    {
        var mandaditos = await _mandaditoService.GetHistoryAsync(userId);
        return Ok(mandaditos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MandaditoRequestDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var mandadito = await _mandaditoService.CreateAsync(dto);
        
            if (mandadito is null) return BadRequest("No se pudo crear el mandadito correctamente");

            return CreatedAtAction(nameof(GetMandaditoById), new { id = mandadito.Id }, mandadito);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<Dictionary<string, List<MandaditoDetailDto>>>>> GetHistoryMandaditos()
    {
        var history = await _mandaditoService.Execute();
    
        var result = history
            .Select(kvp => new Dictionary<string, List<MandaditoDetailDto>>
            {
                {
                    kvp.Key,
                    kvp.Value.Select(m => new MandaditoDetailDto
                    {
                        Id = m.Id,
                        Title = m.Post?.Title ?? "No title",
                        Description = m.Post?.Description ?? string.Empty,
                        AcceptedRate = m.AcceptedRate,
                        AcceptedAt = m.AcceptedAt,
                        DeliveredAt = m.DeliveredAt,
                        SecurityCode = m.SecurityCode,
                        PickupLocation = m.Post?.PickUpLocation != null ? new LocationDto
                        {
                            Id = m.Post.PickUpLocation.Id,
                            Name = m.Post.PickUpLocation.Name,
                        } : null,
                        DeliveryLocation = m.Post?.DeliveryLocation != null ? new LocationDto
                        {
                            Id = m.Post.DeliveryLocation.Id,
                            Name = m.Post.DeliveryLocation.Name,
                        } : null
                    }).ToList()
                }
            })
            .ToList();

        return Ok(result);
    }

    [HttpGet("history/runner")]
    public async Task<ActionResult<IEnumerable<Dictionary<string, List<MandaditoDetailDto>>>>> GetHistoryMandaditosAsRunner()
    {
        var history = await _mandaditoService.ExecuteGet();
    
        var result = history
            .Select(kvp => new Dictionary<string, List<MandaditoDetailDto>>
            {
                {
                    kvp.Key,
                    kvp.Value.Select(m => new MandaditoDetailDto
                    {
                        Id = m.Id,
                        Title = m.Post?.Title ?? "No title",
                        Description = m.Post?.Description ?? string.Empty,
                        AcceptedRate = m.AcceptedRate,
                        AcceptedAt = m.AcceptedAt,
                        DeliveredAt = m.DeliveredAt,
                        SecurityCode = m.SecurityCode,
                        PickupLocation = m.Post?.PickUpLocation != null ? new LocationDto
                        {
                            Id = m.Post.PickUpLocation.Id,
                            Name = m.Post.PickUpLocation.Name,
                        } : null,
                        DeliveryLocation = m.Post?.DeliveryLocation != null ? new LocationDto
                        {
                            Id = m.Post.DeliveryLocation.Id,
                            Name = m.Post.DeliveryLocation.Name,
                        } : null
                    }).ToList()
                }
            })
            .ToList();

        return Ok(result);
    }

}