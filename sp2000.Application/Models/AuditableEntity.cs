using sp2000.Application.Models;

public abstract class AuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}