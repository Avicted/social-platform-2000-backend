using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DTO;

public class CategoryDto : BaseEntity
{
    public int CategoryId { get; set; }
    public string? Title { get; set; }
    public int PostsCount { get; set; }
}