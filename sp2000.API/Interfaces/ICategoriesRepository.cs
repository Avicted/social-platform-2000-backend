using social_platform_2000_backend.Interfaces;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.DataAccessLayer;

public interface ICategoriesRepository : IRepositoryBase<Category>
{
    Task<IEnumerable<CategoryDto>> GetAllGategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);
}