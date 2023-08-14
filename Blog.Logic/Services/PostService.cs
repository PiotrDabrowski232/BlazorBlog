using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserPostRepository _userPostRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IMapper mapper, IUserPostRepository userPostRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userPostRepository = userPostRepository;
            _userRepository = userRepository;
        }

        public void Add(PostDto postDto)
        {
            var post = _mapper.Map<Posts>(postDto);
            post.Id = Guid.NewGuid();

            UserPosts userPost = new UserPosts()
            {
                IdPost = post.Id,
                posts = post,
                IdUser = Guid.Parse("14a92f1c-9110-4604-a96f-0452675a2050"),
                users = _userRepository.GetAll().FirstOrDefault(u => u.Id.Equals(Guid.Parse("14a92f1c-9110-4604-a96f-0452675a2050"))),
            };

            _userPostRepository.Add(userPost);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public PostDto Edit(PostDto post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostDto> GetAll()
        {
            var posts = _postRepository.GetAll();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}
