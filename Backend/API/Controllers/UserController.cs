using Aplication.DTOs.Users;
using Aplication.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
    public readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromForm] UserRequestDTO userRequest)
    {
        if (userRequest.Photo == null || userRequest.Photo.Length == 0)
            return BadRequest("No file uploaded.");
        try
        {
            var user = await _userService.CreateUserAsync(userRequest);
        
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var res = await _userService.GetByIdAsync(id);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}