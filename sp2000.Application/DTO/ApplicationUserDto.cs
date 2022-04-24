
using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class ApplicationUserDto : BaseEntity
{
    public Guid Id { get; set; }
    public virtual string Username { get; set; } = null!;
}