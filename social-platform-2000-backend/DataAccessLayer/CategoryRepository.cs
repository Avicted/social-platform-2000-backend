

using Microsoft.EntityFrameworkCore;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.DataAccessLayer;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<CategoryDto>> GetAllGategoriesAsync()
    {
        return await FindAll()
            .Include(c => c.Posts)
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                PostsCount = c.Posts.Count,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            })
            .ToListAsync();
    }
    public void CreateCategory(Category category)
    {
        Create(category);
    }

    public void DeleteCategory(Category category)
    {
        Delete(category);
    }


    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await FindByCondition(c => c.CategoryId == id)
            .FirstOrDefaultAsync();
    }

    public void UpdateCategory(Category category)
    {
        Update(category);
    }
}