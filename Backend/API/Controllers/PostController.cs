using Aplication.Interfaces.Posts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<IActionResult> GetNear([FromQuery] double lat,[FromQuery] double lon)
    {
        var posts = await _postService.GetAllNearAsync(lat, lon);
        return Ok(posts);
    }
    
}