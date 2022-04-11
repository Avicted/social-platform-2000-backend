using AutoMapper;
using sp2000.Application.DTO;
using sp2000.Application.Models;

namespace sp2000.Mappings;

public class CommentsMapping : Profile
{
    public CommentsMapping()
    {
        CreateMap<Comment, CreateCommentDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<CreateCommentDto, CommentDto>();
    }
}