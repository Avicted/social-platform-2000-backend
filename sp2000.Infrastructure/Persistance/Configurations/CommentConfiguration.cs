using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sp2000.Application.Models;

namespace sp2000.Infrastructure.Persistance.Configurations
{
    internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder
                .Property(p => p.PostId)
                .IsRequired();

            builder
                .Property(p => p.AuthorName)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(p => p.Content)
                .HasMaxLength(4096)
                .IsRequired();
        }
    }
}
