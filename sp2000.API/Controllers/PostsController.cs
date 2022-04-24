using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Interfaces;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;

namespace sp2000.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthorizeAttribute]
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
    [AllowAnonymous]
    public async Task<CustomApiResponse> GetPostByID(int id)
    {
        var post = await _postsService.GetPostByID(id);

        if (post == null)
        {
            // return NotFound("Post not found");
            return new CustomApiResponse(message: "Post not found", statusCode: 404);
        }

        // return Ok(post);
        return new CustomApiResponse(post);
    }

    // PUT: api/Posts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<CustomApiResponse> PutPost(int id, UpdatePostDto post)
    {
        var updatedPost = await _postsService.UpdatePost(id, post);

        if (updatedPost == null)
        {
            // return NotFound("Post not found");
            return new CustomApiResponse(message: "Post not found", statusCode: 404);
        }

        // return Ok(updatedPost);
        return new CustomApiResponse(updatedPost);
    }

    // POST: api/Posts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<CustomApiResponse> CreatePost(CreatePostDto post)
    {
        if (!ModelState.IsValid)
        {
            // return BadRequest(ModelState);
            throw new ApiProblemDetailsException(ModelState);
        }

        var createdPost = await _postsService.CreatePost(post);
        // return CreatedAtAction(nameof(createdPost), createdPost);
        return new CustomApiResponse(createdPost, statusCode: 201);
    }

    // DELETE: api/Posts/5
    [HttpDelete("{id}")]
    public async Task<CustomApiResponse> DeletePost(int id)
    {
        bool isDeleted = await _postsService.DeletePost(id);

        if (!isDeleted)
        {
            // return NotFound("Post not found");
            return new CustomApiResponse(message: "Post not found", statusCode: 404);
        }

        // return NoContent();
        return new CustomApiResponse(statusCode: 204);
    }

    [HttpGet("{postId}/comments")]
    [AllowAnonymous]
    public async Task<CustomApiResponse> GetAllCommentsInPost(int postId)
    {
        var comments = await _commentsService.GetAllCommentsInPost(postId);

        if (comments.Count <= 0)
        {
            // return NotFound("No comments found for the post");
            return new CustomApiResponse(message: "No comments found for the post", statusCode: 404);
        }

        // return Ok(comments);
        return new CustomApiResponse(comments);
    }
}
