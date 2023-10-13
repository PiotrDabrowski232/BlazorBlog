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
        public void Add(PostDto postDto, IList<string>? tags)
        {
            postDto.Id = Guid.NewGuid();
            var post = _mapper.Map<Posts>(postDto);
            post.CreationDate = DateTime.Now;
            post.UserId = _userRepository.GetByEmail(postDto.CreatedBy).Id;

            _postRepository.Add(post);

            if (!tags.IsNullOrEmpty())
                _tagPostsService.AddTagsToPost(tags, post.Id);
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

        public IEnumerable<PostDto> FindPosts(string? postName, IList<string>? tagsName)
        {
            var posts = _postRepository.GetAll();

            IEnumerable<Posts> filteredPosts;

            if (!postName.IsNullOrEmpty())
            {
                posts = posts.Where(p => p.Title == postName);
            }
            if (!tagsName.IsNullOrEmpty())
            {
                var postsIDs = _tagPostsService.GetPostsByTagsName(tagsName);
                posts = posts.Where(p => postsIDs.Contains(p.Id)); // nie daje rezultatów sprawdzić
            }

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }


    #endregion public method
}
