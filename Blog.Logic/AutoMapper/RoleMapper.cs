using AutoMapper;
using Blog.Data.Models;
using Blog.Logic.Dto;

namespace Blog.Logic.AutoMapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleDto, Roles>();
            CreateMap<Roles, RoleDto>();
        }
    }
}
