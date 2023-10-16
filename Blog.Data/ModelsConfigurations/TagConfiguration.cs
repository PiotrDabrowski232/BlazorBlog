using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.ModelsConfigurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    { 
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}