using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sp2000.Application.Models;

namespace Infrastructure.Configuration;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
           .HasMany(p => p.Comments)
           .WithOne();

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder
            .Property(p => p.Content)
            .IsRequired();

        builder
            .Property(p => p.CategoryId)
            .IsRequired();
    }
}