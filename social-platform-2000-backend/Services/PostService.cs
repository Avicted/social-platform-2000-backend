using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PostService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Post?> CreatePost(CreatePostVM post)
    {
        // Does the category exist?
        var category = await _context.Categories.FindAsync(post.CategoryId);

        if (category == null)
        {
            return null;
        }

        var entity = _mapper.Map<Post>(post);

        _context.Posts.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<Post>(post);
    }

    public async Task<List<Post>> GetPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<ApiResponse> GetPostsInCategory(int categoryId, int? pageNumber)
    {
        const int pageSize = 1;

        var posts = _context.Posts.Where(p => p.CategoryId == categoryId);

        if (posts == null)
        {
            return null;
        }

        var items = await posts.Skip(((pageNumber ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();

        // await PaginatedList<Post>.CreateAsync(posts, pageNumber ?? 1, pageSize);

        return new ApiResponse(
            items,
            new Pagination
            {
                CurrentPage = pageNumber ?? 1,
                PageSize = pageSize,
                TotalItemsCount = posts.Count(),
                TotalPages = (int)Math.Ceiling(posts.Count() / (double)pageSize)
            }
        );
    }

    public async Task<Post?> GetPostByID(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<Post?> UpdatePost(int id, Post post)
    {
        // Retrieve entity by id
        var entity = _context.Posts.FirstOrDefault(p => p.PostId == id);

        // Validate entity is not null
        if (entity != null)
        {
            entity.CategoryId = post.CategoryId;
            entity.Title = post.Title;
            entity.Content = post.Content;

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        else
        {
            return null;
        }

        // @Note(Avic): EntityFramework core will have updated the entity at this point
        // So we can return "the same" entity with updated field(s)
        return entity;
    }

    public async Task<bool> DeletePost(int id)
    {
        // Retrieve entity by id
        var entity = _context.Posts.FirstOrDefault(p => p.PostId == id);

        // Validate entity is not null
        if (entity != null)
        {
            _context.Posts.Remove(entity);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        else
        {
            return false;
        }

        return true;
    }
}