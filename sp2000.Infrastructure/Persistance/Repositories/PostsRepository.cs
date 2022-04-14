using Microsoft.EntityFrameworkCore;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;

namespace Infrastructure.Persistance.Repositories;

public class PostsRepository : RepositoryBase<Post>, IPostsRepository
{
    public PostsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreatePost(Post post)
    {
        Create(post);
    }

    public void DeletePost(Post post)
    {
        Delete(post);
    }

    public async Task<IEnumerable<Post>> GetAllPostsInCategoryAsync(int categoryId)
    {
        return await FindByCondition(p => p.CategoryId == categoryId)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await FindByCondition(p => p.PostId == id)
            .FirstOrDefaultAsync();
    }

    public void UpdatePost(Post post)
    {
        Update(post);
    }
}

