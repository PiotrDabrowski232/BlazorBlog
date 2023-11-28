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
using Blog.Data.Models.Enums;
using Blog.Logic.Extensions;
using Microsoft.AspNet.Identity;

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
                Assert.NotEqual(2.ToGuid(), user.RoleId);
            });
        }

        [Fact]
        public void GetUserById_ReturnUserWithConcreteId()
        {
            //Arrange

            //Assert

            //Act
        }
    }
}
