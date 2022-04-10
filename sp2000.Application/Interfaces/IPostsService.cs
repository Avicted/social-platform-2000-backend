using sp2000.Application.DTO;

namespace sp2000.Services;

public interface IPostsService
{
    Task<PostDto> CreatePost(CreatePostDto post);
    Task<List<PostDto>> GetPostsInCategory(int categoryId, int? pageNumber);
    Task<PostDto?> GetPostByID(int id);
    Task<PostDto?> UpdatePost(int id, UpdatePostDto post);
    Task<bool> DeletePost(int id);
}