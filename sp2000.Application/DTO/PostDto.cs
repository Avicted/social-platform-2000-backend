using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class PostDto : BaseEntity
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int CategoryId { get; set; }
    public int CommentsCount { get; set; }
}