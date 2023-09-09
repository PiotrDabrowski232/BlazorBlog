using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(BlogDbContext context) : base(context)
        {
        }

        public User GetById(Guid id)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public User GetByContainedString(string containString)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Email.Equals(containString) || x.UserName.Equals(containString));
        }
        
    }
}
