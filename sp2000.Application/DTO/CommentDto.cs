using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class CommentDto : BaseEntity
{
    public int CommentId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int PostId { get; set; }
    public int? ParentCommentId { get; set; }
    public int ParentsCount { get; set; } = 0;
}
