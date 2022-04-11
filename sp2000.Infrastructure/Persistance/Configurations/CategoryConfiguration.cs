using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sp2000.Application.Models;

namespace Infrastructure.Configuration;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
           .HasMany(c => c.Posts)
           .WithOne();

        builder
            .Navigation(c => c.Posts)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder
            .Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(64);
    }
}