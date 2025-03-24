namespace API.Controllers;

using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/userRole")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;
 
    public UserRoleController(IUserRoleService UserRoleService)
    {
        _userRoleService = UserRoleService;
    }
 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userRoleService.GetAllAsync());
    }
 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var UserRole = await _userRoleService.GetAllAsync();
        return UserRole is null ? NotFound() : Ok(UserRole);
    }
}