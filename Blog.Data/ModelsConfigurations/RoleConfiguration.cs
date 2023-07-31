using Blog.Data.Models;
using Blog.Data.Models.Enums;
using Blog.Logic.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Blog.Data.ModelsConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(x => x.RoleId);

            builder.Property(x => x.Name)
                .IsRequired();

            var roles = new Roles[]
            {
                Create(1.ToGuid(), UserRoles.BasicUser.ToString()),
                Create(2.ToGuid(), UserRoles.Admin.ToString()),
                Create(3.ToGuid(), UserRoles.SuperUser.ToString())
            };

            builder.HasData(roles);
        }

        private static Roles Create(Guid id, string name)
        {
            return new Roles
            {
                RoleId = id,
                Name = name
            };
        }
    }
}