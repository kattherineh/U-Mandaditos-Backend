using Aplication.Interfaces.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    
    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet("near")]
    public async Task<IActionResult> GetNear([FromQuery] int currentLocationId)
    {
        var posts = await _postService.GetAllNearAsync(currentLocationId);
        return Ok(posts);
    }
    
}