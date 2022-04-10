using Microsoft.EntityFrameworkCore;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;

namespace Infrastructure;

public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
{
    public CommentsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateComment(Comment comment)
    {
        Create(comment);
    }

    public void DeleteComment(Comment comment)
    {
        Delete(comment);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsInPostAsync(int postId)
    {
        return await FindByCondition(c => c.PostId == postId)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await FindByCondition(c => c.CommentId == id)
            .FirstOrDefaultAsync();
    }

    public void UpdateComment(Comment comment)
    {
        Update(comment);
    }
}

