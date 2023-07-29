using Blog.Data.Models;

namespace Blog.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();

        public User GetUser();

        public void Add(User user);

        public User Edit(User user);

        public void Delete(int id);
    }
}
