﻿using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Authentication;
using Blog.Logic.Dto;
using Blog.Logic.Dto.UserDtos;
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

        public Task Add(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            user.CreationDate = DateTime.Now;

            return _userRepository.Add(user);
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDelete(string password, UserDto user)
        {
            var samePasswords = _passwordHasher.VerifyHashedPassword(null, user.Password, password);

            if (samePasswords == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid Password");
            }
            else
            {
                _userRepository.SoftDelete(user.Email);
                return Task.CompletedTask;
            }

        }

        public User Edit(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task ChangePassword(PasswordUserDto userDto)
        {
            var user = GetUserByContainedString<User>(userDto.Email);
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userDto.OldPassword);
            
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Incorrect password");
            }
            else
            {
                user.Password = _passwordHasher.HashPassword(null, userDto.NewPassword);
                _userRepository.Update(user);
            }

            return Task.FromResult(result);

        }
        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetUserById<T>(Guid id) where T : class
        {
            return _mapper.Map<T>(_userRepository.GetById(id));
        }

        public T GetUserByContainedString<T>(string email) where T : class
        {
            return _mapper.Map<T>(_userRepository.GetByContainedString(email));
        }


        public LoginUserDto VerifyUser(LoginUserDto LoginDto)
        {
            var user = _mapper.Map<User>(_userRepository.GetByContainedString(LoginDto.Email));

            if (user is null || user.IsDeleted)
            {
                throw new BadRequestException("invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, LoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("invalid username or password");
            }

            LoginDto.Username = user.UserName;
            LoginDto.Role = _mapper.Map<RoleDto>(_roleRepository.Get(user.RoleId));


            return LoginDto;

        }
    }
}