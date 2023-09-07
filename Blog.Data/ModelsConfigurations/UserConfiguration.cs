using Blog.Data.Models;
using Blog.Logic.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.ModelsConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Role);

            builder.Property(u => u.RoleId)
                .HasDefaultValue(1.ToGuid());

            builder.HasMany(u => u.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
