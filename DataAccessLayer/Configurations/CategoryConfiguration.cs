using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DataAccessLayer.Configuration;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
           .HasMany(c => c.Posts)
           .WithOne();

        builder
            .Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(64);
    }
}