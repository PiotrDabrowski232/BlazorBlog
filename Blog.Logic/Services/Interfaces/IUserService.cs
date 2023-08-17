using Blog.Data.Models;
using Blog.Logic.Dto.UserDtos;
using Microsoft.AspNet.Identity;

namespace Blog.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAll();

        public UserDto GetUserByEmail(string email);

        public void Add(UserDto user);

        public User Edit(UserDto user);
        public void Delete(int id);

        public LoginUserDto VerifyUser(LoginUserDto dto);
    }
}