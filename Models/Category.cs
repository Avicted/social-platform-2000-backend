namespace social_platform_2000_backend.Models;

public class Category : BaseEntity
{
    public long CategoryId { get; set; }
    public string? Title { get; set; }
    public List<Post>? Posts { get; set; }
}