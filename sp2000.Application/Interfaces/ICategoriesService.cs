using sp2000.Models;
using sp2000.Application.DTO;

namespace sp2000.Services;

public interface ICategoriesService
{
    Task<CategoryDto> CreateCategory(CreateCategoryDto category);
    Task<List<CategoryDto>> GetCategories(int? pageNumber);
    Task<CategoryDto?> GetCategoryByID(int id);
    Task<CategoryDto?> UpdateCategory(int id, UpdateCategoryDto category);
    Task<bool> DeleteCategory(int id);
}