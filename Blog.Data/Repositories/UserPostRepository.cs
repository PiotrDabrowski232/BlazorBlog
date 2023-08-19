using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class UserPostRepository: GenericRepository<UserPosts>, IGenericRepository<UserPosts>, IUserPostRepository
    {
        public UserPostRepository(BlogDbContext context) : base(context)
        {
        }


        public IEnumerable<UserPosts> GetPostByUserId(Guid id)
        {
            return _context.Set<UserPosts>().Where(x => x.IdUser == id).ToList();
        }

        public IEnumerable<UserPosts> GetUsersByPostId(Guid id)
        {
            return _context.Set<UserPosts>().Where(x => x.IdPost == id).ToList();
        }
    }
}
