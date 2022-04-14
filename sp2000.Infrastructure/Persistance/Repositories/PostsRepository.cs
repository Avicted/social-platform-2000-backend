using Microsoft.EntityFrameworkCore;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;
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

    public async Task<PagedList<PostDto>> GetAllPostsInCategoryAsync(PostParameters postParameters, int categoryId)
    {
        var source = FindByCondition(p => p.CategoryId == categoryId)
            .OrderByDescending(p => p.CreatedDate)
            .Select(p => new PostDto
            {
                CategoryId = p.CategoryId,
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
            });

        return await PagedList<PostDto>.ToPagedListAsync(source, postParameters.PageNumber, postParameters.PageSize);
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

