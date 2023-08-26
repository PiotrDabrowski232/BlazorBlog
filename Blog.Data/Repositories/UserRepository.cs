using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(BlogDbContext context) : base(context)
        {
        }

        public User GetByUserEmail(string email)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Email.Equals(email));
        }

    }
}
