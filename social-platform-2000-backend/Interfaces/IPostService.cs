using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Services;

public interface IPostService
{
    Task<PostDto> CreatePost(CreatePostDto post);
    Task<CustomApiResponse> GetPostsInCategory(int categoryId, int? pageNumber);
    Task<PostDto?> GetPostByID(int id);
    Task<PostDto?> UpdatePost(int id, UpdatePostDto post);
    Task<bool> DeletePost(int id);

}