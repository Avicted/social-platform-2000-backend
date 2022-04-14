using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Models;

namespace sp2000.Application.Interfaces;

public interface IPostsService
{
    Task<PostDto> CreatePost(CreatePostDto post);
    Task<PagedList<PostDto>> GetPostsInCategory(PostParameters postParameters, int categoryId);
    Task<PostDto?> GetPostByID(int id);
    Task<PostDto?> UpdatePost(int id, UpdatePostDto post);
    Task<bool> DeletePost(int id);
}