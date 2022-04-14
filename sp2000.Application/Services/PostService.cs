using sp2000.Application.Models;
using AutoMapper;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;
using sp2000.Application.Helpers;

namespace sp2000.Application.Services;

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


    public async Task<PagedList<PostDto>> GetPostsInCategory(PostParameters postParameters, int categoryId)
    {
        var posts = await _repository.Post.GetAllPostsInCategoryAsync(postParameters, categoryId);

        if (posts == null)
        {
            return new PagedList<PostDto>(new List<PostDto>(), 0, 0, 0);
        }

        return posts;
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