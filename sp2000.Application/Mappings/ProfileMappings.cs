using AutoMapper;
using sp2000.Models;
using sp2000.DTO;

namespace sp2000.Mappings;

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