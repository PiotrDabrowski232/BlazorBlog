﻿using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Posts> post { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Tag> tag { get; set; }
        public DbSet<TagPosts> tagPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        }
    }
}
