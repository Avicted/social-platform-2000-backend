using sp2000.Application.DTO;

namespace sp2000.Application.Interfaces;
public interface ICommentsService
{
    Task<CommentDto> CreateComment(CreateCommentDto comment);
    Task<List<CommentDto>> GetAllCommentsInPost(int postId);
    Task<CommentDto?> GetCommentByID(int id);
    Task<CommentDto?> UpdateComment(int id, UpdateCommentDto comment);
    Task<bool> DeleteComment(int id);
}
