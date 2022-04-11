using sp2000.Interfaces;
using sp2000.Models;

namespace Infrastructure;

public interface IPostsRepository : IRepositoryBase<Post>
{
    Task<IEnumerable<Post>> GetAllPostsInCategoryAsync(int categoryId);
    Task<Post?> GetPostByIdAsync(int id);
    void CreatePost(Post post);
    void UpdatePost(Post post);
    void DeletePost(Post post);
}