namespace social_platform_2000_backend.Models;

public class CreatePostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int CategoryId { get; set; }
}