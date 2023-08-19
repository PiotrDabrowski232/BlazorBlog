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
                IdUser = _userRepository.GetAll().FirstOrDefault(u => u.Email.Equals(postDto.CreatedBy)).Id,
                users = _userRepository.GetAll().FirstOrDefault(u => u.Email.Equals(postDto.CreatedBy)),
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
            var posts = _mapper.Map<IEnumerable<PostDto>>(_postRepository.GetAll());

            foreach(var post in posts)
            {
                var userId = _userPostRepository.GetUsersByPostId(post.Id);

                post.CreatedBy = _userRepository.Get(userId.First().IdUser).UserName;
                
            }

            return posts;
        }

        public IEnumerable<PostDto> GetAllEditableAndDeletableByUser(string userEmail)
        {
            var posts = GetAll();
            List<PostDto> result = new List<PostDto>();

            var user = _userRepository.GetAll().FirstOrDefault(u => userEmail == u.Email);

            foreach (var post in posts)
            {
                if(post.CreatedBy == user.UserName)
                {
                    result.Add(post);
                }
            }

            return result;
        }
    }
}
