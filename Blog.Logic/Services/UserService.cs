using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Exceptions;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;


        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
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

        public UserDto GetUser(string email)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => email == u.Email);
            return _mapper.Map<UserDto>(user);
        }

        public string VerifyUser(LoginUserDto LoginDto)
        {
            var user = _mapper.Map<User>(GetUser(LoginDto.Email));

            if (user is null)
            {
                throw new BadRequestException("invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, LoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("invalid username or password");
            }
            return "verify";
        }
    }
}