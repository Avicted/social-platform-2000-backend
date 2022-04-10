

using Microsoft.EntityFrameworkCore;
using sp2000.Models;
using sp2000.Application.DTO;

namespace Infrastructure;

public class CategoriesRepository : RepositoryBase<Category>, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
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


    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await FindByCondition(c => c.CategoryId == id)
            .FirstOrDefaultAsync();
    }

    public void UpdateCategory(Category category)
    {
        Update(category);
    }
}