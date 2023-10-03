using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Dto.UserDtos;
using Blog.Logic.Exceptions;
using Blog.Logic.Extensions;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

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

        private IEnumerable<T> GetAll<T>() where T : class
        {
            return _mapper.Map<IEnumerable<T>>(_userRepository.GetAll());
        }

        public Task Add(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            user.Password = _passwordHasher.HashPassword(user, user.Password);

            return _userRepository.Add(user);
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDelete(string password, UserDto userDto)
        {
            var samePasswords = _passwordHasher.VerifyHashedPassword(null, userDto.Password, password);

            if (samePasswords == PasswordVerificationResult.Failed)
            {
                throw new InvalidInputException("Invalid Password");
            }
            else
            {
                var user = _mapper.Map<User>(userDto);
                user.DeleteDay = DateTime.Now;
                user.IsDeleted= true;
                _userRepository.Update(user);
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
                throw new InvalidInputException("Incorrect password");
            }
            else
            {
                user.Password = _passwordHasher.HashPassword(null, userDto.NewPassword);
                _userRepository.Update(user);
            }

            return Task.FromResult(result);

        }

        public IEnumerable<AdminUserManagementDto> GetAllNormalUsers()
        {
            var roles = _roleRepository.GetAll().ToDictionary(r => r.RoleId, r => r.Name);

            var filteredUsers = GetAll<AdminUserManagementDto>()
                .Where(u => u.RoleId != 2.ToGuid())
                .ToList(); 

            foreach (var user in filteredUsers)
            {
                if (roles.TryGetValue(user.RoleId, out var roleName))
                {
                    user.RoleName = roleName;
                }
            }

            return filteredUsers;
        }

        public T GetUserById<T>(Guid id) where T : class
        {
            return _mapper.Map<T>(_userRepository.GetById(id));
        }

        public T GetUserByContainedString<T>(string email) where T : class
        {
            return _mapper.Map<T>(_userRepository.GetByEmail(email));
        }

        public LoginUserDto VerifyUser(LoginUserDto LoginDto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email == LoginDto.Email);

            if (user is null || user.IsDeleted)
            {
                throw new InvalidInputException("invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, LoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new InvalidInputException("invalid username or password");
            }

            LoginDto.Username = user.UserName;
            LoginDto.Role = _mapper.Map<RoleDto>(_roleRepository.Get(user.RoleId));

            return LoginDto;
        }

        public  Task<int> GiveAccountBack(Guid id, string password, string adminEmail)
        {
            var userPassword = _userRepository.GetByEmail(adminEmail).Password;

            var result = _passwordHasher.VerifyHashedPassword(null, userPassword, password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new InvalidInputException("invalid username or password");
            }
            else
            {
                return _userRepository.ChangeDeleteStatus(id);
            }
            
        }
    }
}
