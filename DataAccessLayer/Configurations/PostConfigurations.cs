using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DataAccessLayer.Configuration;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Posts);

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