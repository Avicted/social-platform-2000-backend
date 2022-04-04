using AutoMapper;
using Microsoft.EntityFrameworkCore;
using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;
using System.Linq;

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

    public async Task<CustomApiResponse> GetCategories(int? pageNumber)
    {
        var categories = _context.Categories
            .Include(c => c.Posts)
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new CategoryVM
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                PostsCount = c.Posts.Count,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            })
            .AsQueryable();

        if (categories == null)
        {
            return new CustomApiResponse(
                payload: new object(),
                message: "No categories found",
                statusCode: 204
            );
        }

        const int pageSize = 10;
        var temp = await PaginatedList<CategoryVM>.CreateAsync(categories, pageNumber ?? 1, pageSize);
        var result = _mapper.Map<List<CategoryVM>>(temp);


        return new CustomApiResponse(
            payload: result,
            new Pagination
            {
                CurrentPage = pageNumber ?? 1,
                PageSize = pageSize,
                TotalItemsCount = result.Count(),
                TotalPages = (int)Math.Ceiling(result.Count() / (double)pageSize),
            }
        );
    }

    public async Task<CategoryVM?> GetCategoryByID(int id)
    {
        var category = await _context.Categories
            .Where(c => c.CategoryId == id)
            .Include(c => c.Posts)
            .OrderBy(c => c.CreatedDate)
            .Select(c => new CategoryVM
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                PostsCount = c.Posts.Count,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).FirstOrDefaultAsync();

        if (category == null)
        {
            return null;
        }

        var categoryVM = _mapper.Map<CategoryVM>(category);

        return categoryVM;
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