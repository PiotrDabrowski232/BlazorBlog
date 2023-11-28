using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services.Interfaces;
using Blog.Logic.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Logic.Dto.UserDtos;

namespace Blog.UnitTests.ServicesTests
{
    public class UserServiceTest
    {

        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _roleRepository = new Mock<IRoleRepository>();
            _mapper = new Mock<IMapper>();
            _passwordHasher = new Mock<IPasswordHasher<User>>();
            _userService = new UserService(_userRepository.Object, _mapper.Object, _passwordHasher.Object, _roleRepository.Object);
        }

        [Fact]
        public void AddingUser_ReturnCompletedTask()
        {
            // Arrange
            UserDto userDto = new UserDto();
            User user = new User();

            // Act
            _userRepository.Setup(u => u.Add(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = _userService.Add(userDto);

            // Assert
            _userRepository.Verify(u => u.Add(It.IsAny<User>()), Times.Once());

            // Since you're using a new Guid for user.Id, you cannot verify GetById with user.Id
            // Instead, you can verify if GetById is called once with any Guid
            _userRepository.Verify(u => u.GetById(It.IsAny<Guid>()), Times.Once());

            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
