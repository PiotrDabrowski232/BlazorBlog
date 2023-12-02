using AutoMapper;
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
using Blog.Logic.Exceptions;
using Microsoft.Identity.Client;

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
        public void GetUserById_ReturnNotNullUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId
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


        [Fact]
        public void GetUserByContainedString_ReturnedNotNullUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Email = "test@o2.pl",
            };

            _mapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns((User source) => new UserDto
            {
                Email = source.Email,
            });

            _userRepository.Setup(u => u.GetByEmail(user.Email)).Returns(user);


            // Act
            var result = _userService.GetUserByContainedString<UserDto>(user.Email);


            // Assert
            _userRepository.Verify(u => u.GetByEmail(user.Email), Times.Once);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task SoftDelete_ValidPassword_MarksUserAsDeleted()
        {
            // Arrange
            var user = new UserDto()
            {
                Password = "Password123!"
            };

            _passwordHasher.Setup(u => u.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);

            _mapper.Setup(m => m.Map<User>(user)).Returns((UserDto source) => new User
            {
                Id = source.Id,
                UserName = source.UserName,
                IsDeleted = false,
                DeleteDay = null
            });

            _userRepository.Setup(u => u.Update(It.IsAny<User>()));


            // Act
            await _userService.SoftDelete(user.Password, user);


            // Assert
            _passwordHasher.Verify(u => u.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            _userRepository.Verify(u => u.Update(It.Is<User>(u => u.IsDeleted && u.DeleteDay.Value.Date == DateTime.Now.Date)), Times.Once);
        }


        [Fact]
        public async Task SoftDelete_InValidPassword_ThrowsInvalidInputException()
        {
            // Arrange
            var user = new UserDto()
            {
                Password = "InvalidPassword"
            };

            _passwordHasher.Setup(p => p.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Failed);

            //Act

            await Assert.ThrowsAsync<InvalidInputException>(() => _userService.SoftDelete(user.Password, user));

            //Assert

            _userRepository.Verify(u => u.Update(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public void ChangePassword_ReturnedChangedUserPassword()
        {
            //Arrange

            var userPassword = new PasswordUserDto()
            {
                Email = "email@gmail.com", 
                OldPassword = "Test123!",
                NewPassword = "NewTest123!"
            };

            var newHashedPassword = "NewTest123!Hashed";

            var existingUser = new User
            {
                Email = userPassword.Email,
                Password = "HashedPassword" 
            };

            _userRepository.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(existingUser);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(existingUser, existingUser.Password, userPassword.OldPassword)).Returns(PasswordVerificationResult.Success);

            _passwordHasher.Setup(p => p.HashPassword(null, userPassword.NewPassword)).Returns(newHashedPassword);


            //Act

            var result = _userService.ChangePassword(userPassword);

            //Assert
            _userRepository.Verify(u => u.Update(It.Is<User>(u => u.Password == newHashedPassword)),Times.Once);

            Assert.Equal(result.IsCompleted, true);

        }


        [Fact]
        public async Task ChangePassword_ReturnedInvalidInputException()
        {
            //Arrange

            var userPassword = new PasswordUserDto()
            {
                Email = "email@gmail.com",
                OldPassword = "Test123!",
                NewPassword = "NewTest123!"
            };

            var newHashedPassword = "NewTest123!Hashed";

            var existingUser = new User
            {
                Email = userPassword.Email,
                Password = "HashedPassword"
            };

            _userRepository.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(existingUser);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(existingUser, existingUser.Password, userPassword.OldPassword)).Returns(PasswordVerificationResult.Failed);

            //Act

            await Assert.ThrowsAsync<InvalidInputException>(() => _userService.ChangePassword(userPassword));


            //Assert
            _userRepository.Verify(u => u.Update(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public void VerifyUser_Returned_ObjectLoginUser()
        {
            //Arrange
            var loginDto = new LoginUserDto()
            {
                Email = "test@gmail.pl",
            };

            var users = new List<User>()
            {
                new User(){ Email = loginDto.Email}
            };

            _userRepository.Setup(u => u.GetAll()).Returns(users);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);


            //Act

            var result = _userService.VerifyUser(loginDto);


            //Assert

            Assert.Equal(loginDto.Email, result.Email);
        }


        [Fact]
        public void VerifyUser_Returned_InvalidInputException()
        {
            //Arrange
            var loginDto = new LoginUserDto()
            {
                Email = "test@gmail.pl",
            };

            var users = new List<User>()
            {
                new User(){ Email = loginDto.Email}
            };

            _userRepository.Setup(u => u.GetAll()).Returns(users);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Failed);


            //Act

            var exceptionMessage = Assert.Throws<InvalidInputException>(() => _userService.VerifyUser(loginDto));


            //Assert

            Assert.Equal("invalid username or password", exceptionMessage.Message);
        }


        [Fact]
        public void VerifyUser_Returned_IsNullOrDeletedAccount()
        {
            //Arrange
            var loginDto = new LoginUserDto()
            {
                Email = "test@gmail.pl",
            };

            var users = new List<User>()
            {
                new User(){ }
            };

            _userRepository.Setup(u => u.GetAll()).Returns(users);


            //Act

            var exceptionMessage = Assert.Throws<IsNullOrDeletedAccount>(() => _userService.VerifyUser(loginDto));


            //Assert

            Assert.Equal("Account does not exist", exceptionMessage.Message);
        }


        [Fact]
        public void GiveAccountBack_Returned_TaskIntSuccess()
        {
            //Arrange

            var admin = new User()
            {
                Password = "Admin123!",
                Email = "admin@o2.pl",
            };

            _userRepository.Setup(u => u.GetByEmail(admin.Email)).Returns(admin);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);

            _userRepository.Setup( u => u.ChangeDeleteStatus(It.IsAny<Guid>())).Returns(Task.FromResult(0));


            //Act

            var result = _userService.GiveAccountBack(Guid.NewGuid(), admin.Password, admin.Email);

            //Assert

            Assert.Equal(0, result.Result);
        }


        [Fact]
        public async Task GiveAccountBack_Returned_InvalidInputException()
        {
            //Arrange

            var admin = new User()
            {
                Password = "Admin123!",
                Email = "admin@o2.pl",
            };

            _userRepository.Setup(u => u.GetByEmail(admin.Email)).Returns(admin);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Failed);

            _userRepository.Setup(u => u.ChangeDeleteStatus(It.IsAny<Guid>())).Returns(Task.FromResult(0));


            //Act

            var exception = await Assert.ThrowsAsync<InvalidInputException>(() => _userService.GiveAccountBack(It.IsAny<Guid>(),admin.Password, admin.Email));

            //Assert

            Assert.Equal("You have entered the wrong password", exception.Message);
        }


        [Fact]
        public void ResetUserPasswordFromAdminView_Returned_CompletedTaskSuccess()
        {
            //Arrange

            var admin = new User()
            {
                Password = "Admin123!",
                Email = "admin@o2.pl",
            };
            _userRepository.Setup(u => u.GetByEmail(admin.Email)).Returns(admin);

            _passwordHasher.Setup(p => p.VerifyHashedPassword(null, It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);

            _userRepository.Setup(u => u.UpdatingForgottenPassword(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            //Act

            var result = _userService.ResetUserPasswordFromAdminView(It.IsAny<Guid>(),It.IsAny<string>(),admin.Password,admin.Email);

            //Assert

            Assert.Equal(Task.CompletedTask.IsCompletedSuccessfully, result.IsCompletedSuccessfully);
        }


        [Fact]
        public void ResetUserPasswordFromAdminView_Returned_InvalidInputException()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
