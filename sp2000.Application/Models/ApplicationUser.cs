namespace sp2000.Application.Models;

public class ApplicationUser : BaseEntity
{
    // User data
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    // Entities that belong to the user, navigation properties.
    public virtual List<Post> Posts { get; } = null!;
    public virtual List<Comment> Comments { get; } = null!;
}