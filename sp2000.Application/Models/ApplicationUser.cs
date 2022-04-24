namespace sp2000.Application.Models;

public class ApplicationUser : BaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}