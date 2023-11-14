using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.ServicesTest
{
    public class UserServiceTests
    {

        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _roleRepository = new Mock<IRoleRepository>();  
            _mapper = new Mock<IMapper>();
            _passwordHasher = new Mock<IPasswordHasher<User>>();
            _userService = new UserService(_userRepository.Object,  _mapper.Object, _passwordHasher.Object, _roleRepository.Object);
        }


    }
}
