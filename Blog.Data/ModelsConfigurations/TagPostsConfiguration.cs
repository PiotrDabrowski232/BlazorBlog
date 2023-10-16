using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.ModelsConfigurations
{
    public class TagPostsConfiguration : IEntityTypeConfiguration<TagPosts>
    { 
        public void Configure(EntityTypeBuilder<TagPosts> builder)
        {
            builder.HasKey(tp => new { tp.TagId, tp.PostId });

            builder.HasOne(tp => tp.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(tp => tp.PostId);

            builder.HasOne(tp => tp.Tag)
                .WithMany(t => t.Posts)
                .HasForeignKey(tp => tp.TagId);
        }
    }
}