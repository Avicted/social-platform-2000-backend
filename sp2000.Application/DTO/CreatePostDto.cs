namespace sp2000.Application.DTO;

public class CreatePostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int CategoryId { get; set; }
    public Guid ApplicationUserId { get; set; }
}