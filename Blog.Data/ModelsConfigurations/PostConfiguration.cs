using Blog.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.ModelsConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Posts>
    {
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId);
        }
    }
}
