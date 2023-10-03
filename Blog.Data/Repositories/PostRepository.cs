using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories
{
    public class PostRepository : GenericRepository<Posts>, IGenericRepository<Posts>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
