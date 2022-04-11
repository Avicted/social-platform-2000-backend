using Microsoft.AspNetCore.Mvc;
using sp2000.Services;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;

namespace sp2000.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostsService _postsService;
    private readonly ICommentsService _commentsService;

    public PostsController(IPostsService postsService, ICommentsService commentsService)
    {
        _postsService = postsService;
        _commentsService = commentsService;
    }

    // GET: api/Posts/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostByID(int id)
    {
        var post = await _postsService.GetPostByID(id);

        if (post == null)
        {
            return NotFound("Post not found");
        }

        return Ok(post);
    }

    // PUT: api/Posts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPost(int id, UpdatePostDto post)
    {
        var updatedPost = await _postsService.UpdatePost(id, post);

        if (updatedPost == null)
        {
            return NotFound("Post not found");
        }

        return Ok(updatedPost);
    }

    // POST: api/Posts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostDto post)
    {
        if (post.Title == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPost = await _postsService.CreatePost(post);
        return CreatedAtAction(nameof(createdPost), createdPost);
    }

    // DELETE: api/Posts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        bool isDeleted = await _postsService.DeletePost(id);

        if (!isDeleted)
        {
            return NotFound("Post not found");
        }

        return NoContent();
    }

    [HttpGet("{postId}/comments")]
    public async Task<IActionResult> GetAllCommentsInPost(int postId)
    {
        var comments = await _commentsService.GetAllCommentsInPost(postId);

        if (comments.Count <= 0)
        {
            return NotFound("No comments found for the post");
        }

        return Ok(comments);
    }
}
