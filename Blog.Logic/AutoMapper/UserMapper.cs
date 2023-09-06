using AutoMapper;
using Blog.Data.Models;
using Blog.Logic.Dto.UserDtos;

namespace Blog.Logic.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, LoginUserDto>();
            CreateMap<LoginUserDto, User>();

            CreateMap<User, PasswordUserDto>();
            CreateMap<PasswordUserDto, User>();
        }
    }
}
