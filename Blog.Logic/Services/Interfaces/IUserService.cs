using Blog.Data.Models;
using Blog.Logic.Dto;
using Microsoft.AspNet.Identity;

namespace Blog.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAll();

        public UserDto GetUser(string email);

        public void Add(UserDto user);

        public User Edit(UserDto user);
        public void Delete(int id);

        public string VerifyUser(LoginUserDto dto);
    }
}