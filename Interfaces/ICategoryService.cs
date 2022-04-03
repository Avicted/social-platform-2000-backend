using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.Services;

public interface ICategoryService
{
    Task<Category> CreateCategory(Category category);
    Task<List<Category>> GetCategories();
    Task<Category?> GetCategoryByID(int id);
    Task<Category?> UpdateCategory(int id, Category category);
    Task<bool> DeleteCategory(int id);
}