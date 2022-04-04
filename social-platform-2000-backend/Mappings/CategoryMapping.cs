using AutoMapper;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryVM>();
        CreateMap<CreateCategoryVM, Category>();
        CreateMap<CreateCategoryVM, CategoryVM>();
    }
}