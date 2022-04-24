using Microsoft.EntityFrameworkCore;
using sp2000.Application.Models;
using Duende.IdentityServer.EntityFramework.Entities;

namespace sp2000.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<Post> Posts { get; }
    DbSet<Category> Categories { get; }
    DbSet<Comment> Comments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}