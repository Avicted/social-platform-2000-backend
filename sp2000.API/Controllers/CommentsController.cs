using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;
using sp2000.Application.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace sp2000.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    // POST: api/Comments
    [HttpPost]
    public async Task<CustomApiResponse> CreateComment(CreateCommentDto comment)
    {
        if (!ModelState.IsValid)
        {
            // return BadRequest(ModelState);
            throw new ApiProblemDetailsException(ModelState);
        }

        var createdComment = await _commentsService.CreateComment(comment);

        // return CreatedAtAction(nameof(CreateComment), comment);
        return new CustomApiResponse(comment, statusCode: 201);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<CustomApiResponse> GetCommentByID(int id)
    {
        var comment = await _commentsService.GetCommentByID(id);

        if (comment == null)
        {
            // return NotFound("Comment not found");
            return new CustomApiResponse(message: "Comment not found", statusCode: 404);
        }

        // return Ok(comment);
        return new CustomApiResponse(comment);
    }

    // PUT: api/Comments/5
    [HttpPut("{id}")]
    public async Task<CustomApiResponse> UpdateComment(int id, UpdateCommentDto comment)
    {
        var updatedComment = await _commentsService.UpdateComment(id, comment);

        if (updatedComment == null)
        {
            // return NotFound("Comment not found");
            return new CustomApiResponse(message: "Comment not found", statusCode: 404);
        }

        // return Ok(updatedComment);
        return new CustomApiResponse(updatedComment);
    }

    // DELETE: api/Comments/5
    [HttpDelete("{id}")]
    public async Task<CustomApiResponse> DeleteComment(int id)
    {
        bool isDeleted = await _commentsService.DeleteComment(id);

        if (!isDeleted)
        {
            // return NotFound("Comment not found");
            return new CustomApiResponse(message: "Comment not found", statusCode: 404);
        }

        // return NoContent();
        return new CustomApiResponse(statusCode: 204);
    }
}