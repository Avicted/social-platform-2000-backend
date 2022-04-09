namespace sp2000.Models;

// @Note(Avic): Used for adding common fields to models / entities
public class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}