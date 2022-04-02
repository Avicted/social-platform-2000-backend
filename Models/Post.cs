namespace social_platform_2000_backend.Models;

public class Post : BaseEntity
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}