using sp2000.Models;
using AutoMapper;
using sp2000.Application.DTO;
using sp2000.Interfaces;
using Microsoft.Extensions.Configuration;

namespace sp2000.Services;

public class PostService : IPostsService
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public PostService(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<PostDto> CreatePost(CreatePostDto post)
    {
        var entity = _mapper.Map<Post>(post);

        _repository.Post.CreatePost(entity);

        await _repository.SaveAsync();

        return _mapper.Map<PostDto>(entity);
    }


    public async Task<List<PostDto>> GetPostsInCategory(int categoryId, int? pageNumber)
    {
        var posts = await _repository.Post.GetAllPostsInCategoryAsync(categoryId);

        if (posts == null)
        {
            return new List<PostDto>();
        }

        // var items = await posts.Skip(((pageNumber ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
        // const int pageSize = 10;
        // var items = await PaginatedList<Post>.CreateAsync(posts, pageNumber ?? 1, pageSize);

        var result = _mapper.Map<List<PostDto>>(posts);

        return result;
    }

    public async Task<PostDto?> GetPostByID(int id)
    {
        var post = await _repository.Post.GetPostByIdAsync(id);

        if (post == null)
        {
            return null;
        }

        var postDto = _mapper.Map<PostDto>(post);

        return postDto;
    }

    public async Task<PostDto?> UpdatePost(int id, UpdatePostDto post)
    {
        // Retrieve entity by id
        var entity = await _repository.Post.GetPostByIdAsync(id);

        // Validate entity is not null
        if (entity != null)
        {
            entity.CategoryId = post.CategoryId;
            entity.Title = post.Title;
            entity.Content = post.Content;

            _repository.Post.UpdatePost(entity);

            // Save changes to the database
            await _repository.SaveAsync();

            // @Note(Avic): EntityFramework core will have updated the entity at this point
            // So we can return "the same" entity with updated field(s)
            return _mapper.Map<PostDto>(entity);
        }

        return null;
    }

    public async Task<bool> DeletePost(int id)
    {
        // Retrieve entity by id
        var entity = await _repository.Post.GetPostByIdAsync(id);

        // Validate entity is not null
        if (entity != null)
        {
            _repository.Post.DeletePost(entity);

            // Save changes to the database
            await _repository.SaveAsync();

            return true;
        }

        return false;
    }
}