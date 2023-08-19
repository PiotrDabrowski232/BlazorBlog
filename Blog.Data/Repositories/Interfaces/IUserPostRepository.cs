using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IUserPostRepository : IGenericRepository<UserPosts>
    {
        public IEnumerable<UserPosts> GetUsersByPostId(Guid id);
        public IEnumerable<UserPosts> GetPostByUserId(Guid id);
    }
}
