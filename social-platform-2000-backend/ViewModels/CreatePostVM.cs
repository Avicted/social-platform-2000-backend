namespace social_platform_2000_backend.Models;

public class CreatePostVM
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int CategoryId { get; set; }
}