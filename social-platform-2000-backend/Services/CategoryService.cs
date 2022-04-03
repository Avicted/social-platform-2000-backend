using Microsoft.EntityFrameworkCore;
using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Category> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<List<Category>> GetCategories()
    {
        return await _context.Categories.Include(c => c.Posts).ToListAsync();
    }

    public async Task<Category?> GetCategoryByID(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> UpdateCategory(int id, Category category)
    {
        // Retrieve entity by id
        var entity = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

        // Validate entity is not null
        if (entity != null)
        {
            entity.Title = category.Title;

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

    public async Task<bool> DeleteCategory(int id)
    {
        // Retrieve entity by id
        var entity = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

        // Validate entity is not null
        if (entity != null)
        {
            _context.Categories.Remove(entity);

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