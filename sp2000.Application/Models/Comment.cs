using sp2000.Models;

namespace sp2000.Application.Models;

public class Comment : BaseEntity
{
    public int CommentId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Content { get; set; } = null!;
    // @Note(Avic): EFCore convention
    // Navigation property
    public int PostId { get; set; }

    public int? ParentCommentId { get; set; }
}
