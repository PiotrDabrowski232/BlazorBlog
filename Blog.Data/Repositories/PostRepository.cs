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
    public class PostRepository : GenericRepository<Posts>, IGenericRepository<Posts>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
    {
    }
}
}
