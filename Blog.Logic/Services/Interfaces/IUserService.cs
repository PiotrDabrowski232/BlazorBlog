using Blog.Data.Models;
using Blog.Logic.Dto;

namespace Blog.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAll();

        public UserDto GetUser();

        public void Add(UserDto user);

        public User Edit(UserDto user);

        public void Delete(int id);
    }
}
