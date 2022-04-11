namespace sp2000.Application.Models;

public class Category : BaseEntity
{
    // @Note(Avic): EFCore convention "ClassNameId"
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public List<Post> Posts { get; set; } = null!;
}