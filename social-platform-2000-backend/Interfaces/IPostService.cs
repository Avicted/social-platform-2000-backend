using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.Services;

public interface IPostService
{
    Task<Post> CreatePost(Post post);
    Task<List<Post>> GetPosts();
    Task<Post?> GetPostByID(int id);
    Task<Post?> UpdatePost(int id, Post post);
    Task<bool> DeletePost(int id);

}