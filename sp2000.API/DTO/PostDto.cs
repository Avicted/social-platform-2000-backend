using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DTO;

public class PostDto : BaseEntity
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int CategoryId { get; set; }
}