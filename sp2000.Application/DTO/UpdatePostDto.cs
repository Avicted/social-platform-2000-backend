namespace sp2000.DTO;

public class UpdatePostDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int CategoryId { get; set; }
}