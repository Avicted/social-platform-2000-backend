using Microsoft.EntityFrameworkCore;
using sp2000.Application.Models;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;
using sp2000.Application.Helpers;

namespace sp2000.Infrastructure.Persistance.Repositories;

public class CategoriesRepository : RepositoryBase<Category>, ICategoriesRepository
{
    public CategoriesRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParameters categoryParameters)
    {
        var source = FindAll()
            .Include(c => c.Posts)
            .OrderByDescending(c => c.CreatedDate)
            .Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                PostsCount = c.Posts.Count,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            });

        return await PagedList<CategoryDto>.ToPagedListAsync(source, categoryParameters.PageNumber, categoryParameters.PageSize);
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