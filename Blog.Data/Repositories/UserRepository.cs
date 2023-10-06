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

        public User GetByEmail(string Email)
        {
            return _context.Set<User>().First(x => Email == x.Email);
        }

        public Task<int> ChangeDeleteStatus(Guid id)
        {
            var user = _context.Set<User>().FirstOrDefault(u => u.Id == id);
            user.IsDeleted = false;
            _context.Update(user);

            return _context.SaveChangesAsync();
        }

        public Task UpdatingForgottenPassword(Guid id, string userPassword)
        {
            var user = _context.Set<User>().Find(id);
            user.Password = userPassword;
            _context.Update(user);

            return _context.SaveChangesAsync();
        }

    }
}
