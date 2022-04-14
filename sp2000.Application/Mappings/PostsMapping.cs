using AutoMapper;
using sp2000.Application.Models;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;

namespace sp2000.Mappings;

public class PostsMapping : Profile
{
    public PostsMapping()
    {
        CreateMap<Post, CreatePostDto>();
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<CreatePostDto, PostDto>();
    }
}