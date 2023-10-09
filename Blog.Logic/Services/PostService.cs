using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto.PostDtos;
using Blog.Logic.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITagPostsService _tagPostsService;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository,
            ITagPostsService tagPostsService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _tagPostsService = tagPostsService;
        }

        #region private methods

        #endregion private methods


        #region public methods
        public void Add(PostDto postDto)
        {
            postDto.Id = Guid.NewGuid();
            var post = _mapper.Map<Posts>(postDto);
            post.CreationDate = DateTime.Now;
            post.UserId = _userRepository.GetByEmail(postDto.CreatedBy).Id;

            _postRepository.Add(post);

        }

        public void Delete(Guid id)
        {
            _postRepository.Remove(_mapper.Map<Posts>(GetByPostId<PostDto>(id.ToString())));
        }

        public void UpdatePost(PostDto postDto)
        {
            var post = _mapper.Map<Posts>(postDto);
            post.UserId = _postRepository.Get(post.Id).UserId;
            post.CreationDate = DateTime.Now;
            _postRepository.Update(post);
        }

        public IEnumerable<PostDto> GetAll()
        {
            var user = _userRepository.GetAll().Where(u => !u.IsDeleted).ToDictionary(u => u.Id, u => u.Surname);

            var postsDto = _mapper.Map<IEnumerable<PostDto>>(_postRepository.GetAll().Where(p => user.Keys.Contains(p.UserId)));

            foreach (var post in postsDto)
            {
                post.CreatedBy = user.FirstOrDefault(x => x.Key.Equals(post.UserId)).Value;
                
            }
            return postsDto;
        }

        public Task<IEnumerable<PostDto>> GetAllEditableAndDeletableByUser(string userEmail)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => userEmail == u.Email);

            var result = GetAll().Where(post => post.UserId == user.Id);

            return Task.FromResult(result);
        }

        public T GetByPostId<T>(string Id) where T : class
        {
            var post = _mapper.Map<T>(GetAll().FirstOrDefault(p => Id.Equals(p.Id.ToString())));
            
            return post;
        }
    }
    #endregion public method
}
