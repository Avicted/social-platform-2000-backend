using sp2000.Application.Models;
using sp2000.Interfaces;

namespace sp2000.Application.Interfaces;
public interface ICommentsRepository : IRepositoryBase<Comment>
{
    Task<IEnumerable<Comment>> GetAllCommentsInPostAsync(int postId);
    Task<Comment?> GetCommentByIdAsync(int id);
    void CreateComment(Comment comment);
    void UpdateComment(Comment comment);
    void DeleteComment(Comment comment);
}

