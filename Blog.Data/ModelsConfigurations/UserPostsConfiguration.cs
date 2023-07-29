using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.ModelsConfigurations
{
    public class UserPostsConfiguration : IEntityTypeConfiguration<UserPosts>
    {
        public void Configure(EntityTypeBuilder<UserPosts> builder)
        {
            builder.HasKey(up => new { up.IdUser, up.IdPost });
        }
    }
}
