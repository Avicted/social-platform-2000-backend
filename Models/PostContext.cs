

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace social_platform_2000_backend.Models;

public class PostContext : DbContext
{
    public PostContext(DbContextOptions<PostContext> options)
        : base(options)
    {
    }

    public DbSet<Post> TodoItems { get; set; } = null!;
}