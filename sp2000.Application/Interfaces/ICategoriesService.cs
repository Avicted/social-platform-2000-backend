using sp2000.Application.Models;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;

namespace sp2000.Application.Interfaces;

public interface ICategoriesService
{
    Task<CategoryDto> CreateCategory(CreateCategoryDto category);
    Task<PagedList<CategoryDto>> GetCategories(CategoryParameters categoryParameters);
    Task<CategoryDto?> GetCategoryByID(int id);
    Task<CategoryDto?> UpdateCategory(int id, UpdateCategoryDto category);
    Task<bool> DeleteCategory(int id);
}