using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sp2000.Application.Models;

namespace Infrastructure.Configuration;

public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(u => u.Id)
            .HasDefaultValue<Guid>(new Guid())
            .IsRequired();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(12);

        builder
            .HasMany<Post>(u => u.Posts)
            .WithOne();

        builder
            .HasMany<Comment>(u => u.Comments)
            .WithOne();

        builder
            .Navigation(u => u.Comments)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder
            .Navigation(u => u.Posts)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}