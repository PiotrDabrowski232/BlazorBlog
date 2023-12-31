﻿// <auto-generated />
using System;
using Blog.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blog.Data.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20231216145602_VerificationAtributesForUser")]
    partial class VerificationAtributesForUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blog.Data.Models.Posts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("post");
                });

            modelBuilder.Entity("Blog.Data.Models.Roles", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("00000001-0000-0000-0000-000000000000"),
                            Name = "BasicUser"
                        },
                        new
                        {
                            RoleId = new Guid("00000002-0000-0000-0000-000000000000"),
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("00000003-0000-0000-0000-000000000000"),
                            Name = "SuperUser"
                        });
                });

            modelBuilder.Entity("Blog.Data.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("Blog.Data.Models.TagPosts", b =>
                {
                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("tagPosts");
                });

            modelBuilder.Entity("Blog.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeleteDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastLogginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000001-0000-0000-0000-000000000000"));

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VerificationCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000001-0000-0000-0000-000000000000"),
                            City = "City",
                            Country = "Country",
                            Email = "admin@o2.pl",
                            Gender = 0,
                            IsDeleted = false,
                            IsVerified = true,
                            Name = "Admin",
                            Password = "AQAAAAEAACcQAAAAEBDU71c8+fcaiTIOHxOllMb1YQRsFBUzkhV0/zTMHmv9rc4V6hOUA1CVZJobQ1J6Vg==",
                            RoleId = new Guid("00000002-0000-0000-0000-000000000000"),
                            Surname = "Admin",
                            UserName = "Admin",
                            VerificationCode = 0
                        });
                });

            modelBuilder.Entity("Blog.Data.Models.Posts", b =>
                {
                    b.HasOne("Blog.Data.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Data.Models.TagPosts", b =>
                {
                    b.HasOne("Blog.Data.Models.Posts", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Data.Models.Tag", "Tag")
                        .WithMany("Posts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Blog.Data.Models.User", b =>
                {
                    b.HasOne("Blog.Data.Models.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Blog.Data.Models.Posts", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Blog.Data.Models.Tag", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Blog.Data.Models.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
