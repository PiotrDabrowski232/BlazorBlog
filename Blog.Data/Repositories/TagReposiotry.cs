using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class TagReposiotry : GenericRepository<Tag>, IGenericRepository<Tag>, ITagRepository
    {
        public TagReposiotry(BlogDbContext context) : base(context)
        {
        }
    }
}
