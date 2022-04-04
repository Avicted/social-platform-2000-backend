using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Services;

public interface IPostService
{
    Task<Post?> CreatePost(CreatePostVM post);
    Task<List<Post>> GetPosts();
    Task<ApiResponse> GetPostsInCategory(int categoryId, int? pageNumber);
    Task<Post?> GetPostByID(int id);
    Task<Post?> UpdatePost(int id, Post post);
    Task<bool> DeletePost(int id);

}