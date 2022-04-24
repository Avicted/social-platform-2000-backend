namespace sp2000.Application.Models;

// @Note(Avic): Used for adding common fields to models / entities
public class BaseEntity
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}