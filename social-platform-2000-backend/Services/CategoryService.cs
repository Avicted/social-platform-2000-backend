using AutoMapper;
using Microsoft.EntityFrameworkCore;
using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<CategoryVM> CreateCategory(CreateCategoryVM category)
    {
        var entity = _mapper.Map<Category>(category);

        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryVM>(category);
    }

    public async Task<List<CategoryVM>> GetCategories()
    {
        // return await _context.Categories.Include(c => c.Posts).ToListAsync();
        var categoryList = await _context.Categories
        .Include(c => c.Posts)
        .OrderBy(c => c.CreatedDate)
        .Select(c => new CategoryVM
        {
            CategoryId = c.CategoryId,
            Title = c.Title,
            PostsCount = c.Posts.Count,
            CreatedDate = c.CreatedDate,
            UpdatedDate = c.UpdatedDate
        }).ToListAsync();

        var categoryVMList = _mapper.Map<List<CategoryVM>>(categoryList);

        return categoryVMList;
    }

    public async Task<CategoryVM?> GetCategoryByID(int id)
    {
        return _mapper.Map<CategoryVM>(await _context.Categories.FindAsync(id));
    }

    public async Task<CategoryVM?> UpdateCategory(int id, Category category)
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
        return _mapper.Map<CategoryVM>(entity);
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