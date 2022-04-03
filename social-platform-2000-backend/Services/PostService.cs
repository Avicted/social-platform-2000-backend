using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace social_platform_2000_backend.Services;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Post> CreatePost(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<List<Post>> GetPosts()
    {
        return await _context.Posts.ToListAsync();
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