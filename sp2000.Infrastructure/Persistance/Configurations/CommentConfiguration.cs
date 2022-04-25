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
                .Property(c => c.Content)
                .HasMaxLength(10000) // @Note(Avic): Reddit uses 10k
                .IsRequired();

            builder
                .Property(c => c.ParentCommentId)
                .IsRequired(false); // @Note(Avic): A comment without ParentCommentId belongs to the root of the Post
        }
    }
}
