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
    
    [HttpGet("near/posts")]
    public async Task<IActionResult> GetNearPosts([FromQuery] int currentLocationId)
    {
        var posts = await _postService.GetPostsNearLocationAsync(currentLocationId);
        return Ok(posts);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostByUser([FromQuery] int idUser)
    {
        var posts = await _postService.GetPostsByPosterUserIdAsync(idUser);
        return Ok(posts);
    }
    
    [HttpGet("count/{id}")]
    public async Task<IActionResult> GetPostCount([FromQuery] int idUser)
    {
        var posts = await _postService.GetPostsCountAsync(idUser);
        return Ok(posts);
    }
    
    [HttpGet("post/{id}")]
    public async Task<IActionResult> GetPostById([FromQuery] int idPost)
    {
        var post = await _postService.GetPostByIdAsync(idPost);
        return Ok(post);
    }
}