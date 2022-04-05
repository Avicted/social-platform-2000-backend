namespace social_platform_2000_backend.Models;

public class Post : BaseEntity
{
    // @Note(Avic): EFCore convention "ClassBameId"
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    // @Note(Avic): EFCore convention
    // Navigation property
    public int CategoryId { get; set; }
}