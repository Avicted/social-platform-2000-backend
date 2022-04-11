using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class CategoryDto : BaseEntity
{
    public int CategoryId { get; set; }
    public string? Title { get; set; }
    public int PostsCount { get; set; }
}