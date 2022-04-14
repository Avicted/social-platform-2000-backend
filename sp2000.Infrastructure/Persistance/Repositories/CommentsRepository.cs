using Microsoft.EntityFrameworkCore;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;

namespace Infrastructure.Persistance.Repositories;

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
        var comments = await FindByCondition(c => c.PostId == postId)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();

        return comments;

        // For each comment get the count of all parent comments
        /* foreach (var comment in comments)
        {
            while (true)
            {
                var parentComment = await FindByCondition(c => c.)
            }

            return comments;
        } */
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

