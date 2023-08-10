using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blog.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;


        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository= userRepository;
            _mapper= mapper;
            _passwordHasher= passwordHasher;
        }

        public void Add(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            user.Password = _passwordHasher.HashPassword(user, user.Password);

            _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Edit(UserDto user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser()
        {
            throw new NotImplementedException();
        }
    }
}
