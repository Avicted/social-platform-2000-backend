using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Services;

public interface ICategoryService
{
    Task<CategoryVM> CreateCategory(CreateCategoryVM category);
    Task<CustomApiResponse> GetCategories(int? pageNumber);
    Task<CategoryVM?> GetCategoryByID(int id);
    Task<CategoryVM?> UpdateCategory(int id, Category category);
    Task<bool> DeleteCategory(int id);
}