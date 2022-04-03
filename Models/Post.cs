namespace social_platform_2000_backend.Models;

public class Post : BaseEntity
{
    public long? PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    // Navigation properties
    public long CategoryId { get; set; }
    public Category? Category { get; set; }
}