using sp2000.Models;

namespace sp2000.DTO;

public class CategoryDto : BaseEntity
{
    public int CategoryId { get; set; }
    public string? Title { get; set; }
    public int PostsCount { get; set; }
}