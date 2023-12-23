using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using System;
using Blog.Logic.Extensions;

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

            builder.Property(u => u.IsVerified)
                .HasDefaultValue(false);


            builder.HasData(new User()
            {
                Id = 1.ToGuid(),
                UserName = "Admin",
                Name = "Admin",
                Surname = "Admin",
                Email = "admin@o2.pl",
                Password = "AQAAAAEAACcQAAAAEBDU71c8+fcaiTIOHxOllMb1YQRsFBUzkhV0/zTMHmv9rc4V6hOUA1CVZJobQ1J6Vg==", //Admin123!
                RoleId = 2.ToGuid(),
                City = "City",
                Country = "Country",
                IsVerified = true,
            }) ;
        }
    }
}