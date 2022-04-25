namespace sp2000.Application.Models;

public class Post : BaseEntity
{
    // @Note(Avic): EFCore convention "ClassNameId"
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    // @Note(Avic): EFCore convention
    // Navigation property
    public int CategoryId { get; set; }

    public List<Comment> Comments { get; set; } = null!;

}