﻿using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services.Interfaces;
using Blog.Logic.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Blog.Logic.Dto.UserDtos;
using Blog.Data.Models.Enums;
using Blog.Logic.Extensions;
using Blog.Logic.AutoMapper;

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
        public void AddingEmptyUser_ReturnedFaultedResult()
        {
            // Arrange
            UserDto userDto = new UserDto();

            // Act

            var result = _userService.Add(userDto);

            // Assert

            Assert.True(result.IsFaulted);
        }

        [Fact]
        public void GetAllNormalUsers_ReturnedUserWithBasicRole()
        {
            //Areange
            var roles = new List<Roles>()
            {
            new Roles{ RoleId = 1.ToGuid(), Name = UserRoles.BasicUser.ToString() },
            new Roles{ RoleId = 2.ToGuid(), Name = UserRoles.Admin.ToString()},
            new Roles{ RoleId = 3.ToGuid(), Name = UserRoles.SuperUser.ToString()}
            };

            _roleRepository.Setup(l => l.GetAll()).Returns(roles);

            _userRepository.Setup(u => u.GetAll()).Returns(It.IsAny<IEnumerable<User>>);

            //Act

            var result = _userService.GetAllNormalUsers();


            //Assert

            _roleRepository.Verify(r => r.GetAll(), Times.Once);

            _userRepository.Verify(u => u.GetAll(), Times.Once);

            Assert.NotNull(result);

            Assert.All(result, user =>
            {
                Assert.Equal(1.ToGuid(), user.RoleId);
                Assert.NotEqual(2.ToGuid(), user.RoleId);
                Assert.NotEqual(3.ToGuid(), user.RoleId);
            });
        }

        [Fact]
        public void GetUserById_ReturnUserWithConcreteId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Name = "test",
                Surname = "test",
                Email = "test@o2.pl",
                City = "Test",
                Country = "Test",
                UserName = "Test",
                Password = "Test123!",
                IsDeleted = false,
            };

            _mapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns((User source) => new UserDto
            {
                Id = source.Id,
            });

            _userRepository.Setup(u => u.GetById(userId)).Returns(user);

            // Act
            var result = _userService.GetUserById<UserDto>(userId);

            // Assert
            _userRepository.Verify(u => u.GetById(userId), Times.Once);


            Assert.NotNull(result);
        }

        

    }
}
