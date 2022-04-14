using sp2000.Application.Models;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;

namespace sp2000.Application.Interfaces;

public interface ICategoriesRepository : IRepositoryBase<Category>
{
    Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParameters categoryParameters);
    Task<Category?> GetCategoryByIdAsync(int id);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
}