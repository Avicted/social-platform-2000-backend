using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;
using sp2000.Services;

namespace sp2000.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    // POST: api/Comments
    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdComment = await _commentsService.CreateComment(comment);

        return CreatedAtAction(nameof(CreateComment), comment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentByID(int id)
    {
        var comment = await _commentsService.GetCommentByID(id);

        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        return Ok(comment);
    }

    // PUT: api/Comments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto comment)
    {
        var updatedComment = await _commentsService.UpdateComment(id, comment);

        if (updatedComment == null)
        {
            return NotFound("Comment not found");
        }

        return Ok(updatedComment);
    }

    // DELETE: api/Comments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        bool isDeleted = await _commentsService.DeleteComment(id);

        if (!isDeleted)
        {
            return NotFound("Comment not found");
        }

        return NoContent();
    }
}