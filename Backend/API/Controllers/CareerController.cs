using Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/careers")]
public class CareerController : ControllerBase
{
    private readonly ICareerService _careerservice;

    public CareerController(ICareerService careerService)
    {
        _careerservice = careerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _careerservice.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var career = await _careerservice.GetByIdASync(id);
        return career is null ? NotFound() : Ok(career);
    }
}