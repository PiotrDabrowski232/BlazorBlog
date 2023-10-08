using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using System;
using Blog.Logic.Extensions;

namespace Blog.Data.ModelsConfigurations
{
    public class TagPostsConfiguration : IEntityTypeConfiguration<TagPosts>
    { 
        public void Configure(EntityTypeBuilder<TagPosts> builder)
        {
            builder.HasKey(tp => new { tp.TagId, tp.PostId});
        }
    }
}