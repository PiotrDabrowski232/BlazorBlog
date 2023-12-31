﻿using Blog.Data.Models;
using Blog.Logic.Dto.UserDtos;

namespace Blog.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<AdminUserManagementDto> GetAllNormalUsers();
        public Task Add(UserDto user);
        public User Edit(UserDto user);
        public Task ChangePassword(PasswordUserDto userDto);
        public Task SoftDelete(string password, UserDto user);
        public T GetUserById<T>(Guid id) where T : class;
        public T GetUserByContainedString<T>(string email) where T : class;
        public LoginUserDto VerifyUser(LoginUserDto dto);
        public Task<int> GiveAccountBack(Guid id, string password, string adminEmail);
        public Task ResetUserPasswordFromAdminView(Guid id, string Uesrpassword, string adminPassword, string adminName);
        public IEnumerable<User>? CheckUsersToDelete();
        public Task AccountVerification(string email, string code);
    }
}