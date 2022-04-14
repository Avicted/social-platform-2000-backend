using AutoMapper;
using sp2000.Application.DTO;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;

namespace sp2000.Application.Services;

public class CommentsService : ICommentsService
{
    private readonly IRepositoryWrapper _repostiory;
    private readonly IMapper _mapper;

    public CommentsService(IRepositoryWrapper repostiory, IMapper mapper)
    {
        _repostiory = repostiory;
        _mapper = mapper;
    }

    public async Task<CommentDto> CreateComment(CreateCommentDto comment)
    {
        var entity = _mapper.Map<Comment>(comment);

        _repostiory.Comment.CreateComment(entity);

        await _repostiory.SaveAsync();

        return _mapper.Map<CommentDto>(entity);
    }

    public async Task<List<CommentDto>> GetAllCommentsInPost(int postId)
    {
        var comments = await _repostiory.Comment.GetAllCommentsInPostAsync(postId);

        if (comments == null)
        {
            return new List<CommentDto>();
        }

        return _mapper.Map<List<CommentDto>>(comments);
    }

    public async Task<CommentDto?> GetCommentByID(int id)
    {
        var comment = await _repostiory.Comment.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return null;
        }

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<CommentDto?> UpdateComment(int id, UpdateCommentDto comment)
    {
        // Retrieve entity by id
        var entity = await _repostiory.Comment.GetCommentByIdAsync(id);

        if (entity != null)
        {
            // Update entity data, in this case only the content
            entity.Content = comment.Content;

            _repostiory.Comment.UpdateComment(entity);

            // Save changes to the database
            await _repostiory.SaveAsync();

            // @Note(Avic): EntityFramework core will have updated the entity at this point
            // So we can return "the same" entity with updated field(s)
            return _mapper.Map<CommentDto>(entity);
        }

        return null;
    }
    public async Task<bool> DeleteComment(int id)
    {
        // Retrieve entity by id
        var entity = await _repostiory.Comment.GetCommentByIdAsync(id);

        if (entity != null)
        {
            _repostiory.Comment.DeleteComment(entity);

            // Save changes to the database
            await _repostiory.SaveAsync();

            return true;
        }

        return false;
    }

}