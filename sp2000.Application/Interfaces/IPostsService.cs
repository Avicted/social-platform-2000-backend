using sp2000.Models;
using sp2000.DTO;

namespace sp2000.Services;

public interface IPostsService
{
    Task<PostDto> CreatePost(CreatePostDto post);
    Task<CustomApiResponse> GetPostsInCategory(int categoryId, int? pageNumber);
    Task<PostDto?> GetPostByID(int id);
    Task<PostDto?> UpdatePost(int id, UpdatePostDto post);
    Task<bool> DeletePost(int id);

}