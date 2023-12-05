using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
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
            });

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


        [Fact]
        public void UpdatePost_UpadtingPost()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var postdto = new PostDto() { Id = postId };
            var post = new Posts() { Id = postId };

            _mapper.Setup(m => m.Map<Posts>(It.IsAny<PostDto>())).Returns(post);
            _postRepository.Setup(p => p.Get(postId)).Returns(post);
            _postRepository.Setup(p => p.Update(post));

            //Act
            _postService.UpdatePost(postdto);

            //Assert
            _postRepository.Verify(p => p.Get(postId), Times.Once);
            _postRepository.Verify(p => p.Update(post), Times.Once);
        }


        [Fact]
        public void GetAll_GetsAllPosts()
        {
            var users = new List<User>() 
            {
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Surname = "Test"},
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Surname = "Test1"},
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Surname = "Test3"},
            };

            var posts = new List<Posts>()
            {
               new Posts(){ Id = Guid.NewGuid(), UserId = users[0].Id },
               new Posts(){ Id = Guid.NewGuid(), UserId = users[1].Id },
               new Posts(){ Id = Guid.NewGuid(), UserId = users[2].Id },
            };

            _userRepository.Setup(u => u.GetAll()).Returns(users);

            _postRepository.Setup(p => p.GetAll()).Returns(posts);

            _mapper.Setup(m => m.Map<IEnumerable<PostDto>>(It.IsAny<IEnumerable<Posts>>())).Returns((IEnumerable<Posts> source)=> 
                source.Select(post => new PostDto
                {
                    Id = post.Id,
                    UserId = post.UserId,

                }));

            //Act

            var result = _postService.GetAll();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(result.Count(), posts.Count); 
            Assert.IsType<List<PostDto>>(result.ToList());

            _postRepository.Verify(p => p.GetAll(), Times.Once);
            _userRepository.Verify(p => p.GetAll(), Times.Once);
        }


        [Fact]
        public void GetAllEditableAndDeletableByUser_ReturnedUserPosts()
        {
            //Assert
            var users = new List<User>()
            {
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Email = "Test@gmail.com"},
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Email = "Test1@gmail.com"},
            new User(){Id=Guid.NewGuid(),IsDeleted=false, Email = "Test3@gmail.com"},
            };

            var posts = new List<Posts>()
            {
               new Posts(){ Id = Guid.NewGuid(), UserId = users[0].Id },
               new Posts(){ Id = Guid.NewGuid(), UserId = users[0].Id },
               new Posts(){ Id = Guid.NewGuid(), UserId = users[0].Id },
            };



            _userRepository.Setup(u => u.GetAll()).Returns(users);
            _postRepository.Setup(p => p.GetAll()).Returns(posts);


            //Act

            var result = _postService.GetAllEditableAndDeletableByUser(users[0].Email);

            //Assert

            Assert.NotNull(result);
            Assert.IsType<Task<IEnumerable<PostDto>>>(result);
            Assert.Equal(Task.CompletedTask.IsCompleted, result.IsCompleted);

            _userRepository.Verify(u => u.GetAll(), Times.Exactly(2));
            _postRepository.Verify(u => u.GetAll(), Times.Once);
        }
    }
}
