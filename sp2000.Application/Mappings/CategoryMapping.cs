using AutoMapper;
using sp2000.Models;
using sp2000.Application.DTO;

namespace sp2000.Mappings;

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