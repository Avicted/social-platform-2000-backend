using AutoMapper;
using sp2000.Application.Models;
using sp2000.Application.DTO;

namespace sp2000.Mappings;

public class CategoriesMapping : Profile
{
    public CategoriesMapping()
    {
        CreateMap<Category, CreateCategoryDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<CreateCategoryDto, CategoryDto>();
    }
}