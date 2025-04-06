using Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Aplication.DTOs;
using Aplication.DTOs.General;

namespace API.Controllers;

[ApiController]
[Route("api/management")]
public class ManagementController : ControllerBase
{
    private readonly IManagementService _managementService;

    public ManagementController(IManagementService managementService)
    {
        _managementService = managementService;
    }

    [HttpPost("pwd")]
    public async Task<IActionResult> CreateManagementPassword([FromBody] ManagementPasswordRequestDTO dto)
    {
        try
        {
            var res = await _managementService.SendEmailAsync(dto.Email);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPatch("{id}/compare")]
    public async Task<IActionResult> CompareCode(int id, [FromBody] CodeDTO dto)
    {
        try
        {
            var res = await _managementService.CompareCodeAsync(id, dto.Code);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}