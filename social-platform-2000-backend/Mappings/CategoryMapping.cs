using AutoMapper;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CreateCategoryDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<CreateCategoryDto, CategoryDto>();
    }
}