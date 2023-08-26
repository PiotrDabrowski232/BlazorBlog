using AutoMapper;
using Blog.Data.Models;
using Blog.Logic.Dto.PostDtos;

namespace Blog.Logic.AutoMapper
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Posts, PostDto>();
            CreateMap<PostDto, Posts>();

            CreateMap<Posts, EditPostDto>();
            CreateMap<EditPostDto, Posts>();
        }
    }
}
