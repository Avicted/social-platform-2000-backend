using sp2000.Interfaces;
using sp2000.Models;
using sp2000.Application.DTO;

namespace Infrastructure;

public interface ICategoriesRepository : IRepositoryBase<Category>
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
}