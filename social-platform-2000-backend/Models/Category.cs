namespace social_platform_2000_backend.Models;

public class Category : BaseEntity
{
    // @Note(Avic): EFCore convention "ClassBameId"
    public int CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public List<Post> Posts { get; set; } = null!;
}