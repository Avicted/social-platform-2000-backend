namespace social_platform_2000_backend.Models;

// @Note(Avic): Used for adding common fields to models / entities
public class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}