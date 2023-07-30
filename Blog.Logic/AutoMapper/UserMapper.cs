using AutoMapper;
using Blog.Data.Models;
using Blog.Logic.Dto;

namespace Blog.Logic.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
