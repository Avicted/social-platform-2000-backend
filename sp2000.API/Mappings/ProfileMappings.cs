using AutoMapper;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Mappings;

public class PostsMapping : Profile
{
    public PostsMapping()
    {
        CreateMap<Post, CreatePostDto>();
        CreateMap<Post, PostDto>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<CreatePostDto, PostDto>();
    }
}