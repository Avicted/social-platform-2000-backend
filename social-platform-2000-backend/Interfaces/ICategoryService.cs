using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategory(CreateCategoryDto category);
    Task<CustomApiResponse> GetCategories(int? pageNumber);
    Task<CategoryDto?> GetCategoryByID(int id);
    Task<CategoryDto?> UpdateCategory(int id, UpdateCategoryDto category);
    Task<bool> DeleteCategory(int id);
}