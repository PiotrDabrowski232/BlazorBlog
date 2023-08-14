using AutoMapper;
using Blog.Data.Models;
using Blog.Logic.Dto;

namespace Blog.Logic.AutoMapper
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Posts, PostDto>();
            CreateMap<PostDto, Posts>();
        }
    }
}
