using Aplication.DTOs.Media;
using Aplication.Interfaces.Medias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/media")]
    public class MediaController: ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var media = await _mediaService.GetAllAsync();
            return Ok(media);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var media = await _mediaService.GetByIdAsync(id);
            return media is null ? NotFound($"El archivo con id {id} no existe.") : Ok(media);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MediaRequestDTO media)
        {
            var m = await _mediaService.CreateAsync(media);
            return CreatedAtAction( nameof(GetById), new {id = m.Id}, m);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] MediaRequestDTO media)
        {
            var updated = await _mediaService.UpdateAsync(id, media);
            return updated ? NoContent() : Ok(updated); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _mediaService.DeleteAsync(id); 
            return deleted ? NoContent() : NotFound();
        }
    }
}
