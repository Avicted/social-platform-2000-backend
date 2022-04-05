using social_platform_2000_backend.Interfaces;
using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DataAccessLayer;

public interface IPostRepository : IRepositoryBase<Post>
{
    Task<IEnumerable<Post>> GetAllPostsInCategoryAsync(int categoryId);
    Task<Post> GetPostByIdAsync(int id);
    void CreatePost(Post post);
    void UpdatePost(Post post);
    void DeleteCategory(Post post);
}