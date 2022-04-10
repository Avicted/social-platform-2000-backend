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
                .Property(p => p.Content)
                .IsRequired();

            builder
                .Property(p => p.PostId)
                .IsRequired();
        }
    }
}
