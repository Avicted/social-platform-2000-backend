using AutoMapper;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Mappings;

public class ProfileMapping : Profile
{
    public ProfileMapping()
    {
        CreateMap<CreatePostVM, Post>();
    }
}