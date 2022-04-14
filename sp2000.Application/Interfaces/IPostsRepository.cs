using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Models;

namespace sp2000.Application.Interfaces;

public interface IPostsRepository : IRepositoryBase<Post>
{
    Task<PagedList<PostDto>> GetAllPostsInCategoryAsync(PostParameters postParameters, int categoryId);
    Task<Post?> GetPostByIdAsync(int id);
    void CreatePost(Post post);
    void UpdatePost(Post post);
    void DeletePost(Post post);
}