using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto.PostDtos;
using Blog.Logic.Services;
using Blog.Logic.Services.Interfaces;
using Moq;

namespace Blog.UnitTests.ServicesTests
{
    public class PostServiceTest
    {
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITagPostsService> _tagPostsService;
        private readonly PostService _postService;

        public PostServiceTest()
        {
            _postRepository = new Mock<IPostRepository>();
            _mapper = new Mock<IMapper>();
            _userRepository = new Mock<IUserRepository>();
            _tagPostsService = new Mock<ITagPostsService>();
            _postService = new PostService(_postRepository.Object, _mapper.Object, _userRepository.Object, _tagPostsService.Object);
        }

        [Fact]
        public void Add_PostForUser()
        {
            //Arrange

            var currentDateTime = DateTime.Now;
            var post = new Posts();
            var user = new User() { Id = Guid.NewGuid(), Email = "test@gmail.com" };
            var postDto = new PostDto() { Id = Guid.NewGuid(), CreatedBy = user.Email };

            _mapper.Setup(p => p.Map<Posts>(postDto)).Returns((PostDto source) => new Posts
            {
                Description = source.Description,
                Title = source.Title,
                Id = source.Id,
                CreationDate = currentDateTime
            }) ;

            _userRepository.Setup(u => u.GetByEmail(postDto.CreatedBy)).Returns(user);

            _postService.Add(postDto, It.IsAny<List<string>>());


            //Assert

            _userRepository.Verify(u => u.GetByEmail(user.Email), Times.Once);

            _postRepository.Verify(p => p.Add(It.Is<Posts>(post =>
                post.Id == postDto.Id &&
                post.CreationDate.Date == currentDateTime.Date &&  
                post.UserId == user.Id &&
                post.Description == postDto.Description &&
                post.Title == postDto.Title)), Times.Once);
        }
    }
}
