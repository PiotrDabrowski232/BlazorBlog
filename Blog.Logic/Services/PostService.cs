using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Dto.PostDtos;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #region private methods

        #endregion private methods


        #region public methods
        public void Add(PostDto postDto)
        {
            postDto.Id = Guid.NewGuid();
            var post = _mapper.Map<Posts>(postDto);

            post.UserId = _userRepository.GetByContainedString(postDto.CreatedBy).Id;

            _postRepository.Add(post);

        }

        public void Delete(Guid id)
        {
            _postRepository.Remove(_mapper.Map<Posts>(GetByPostId(id.ToString())));
        }

        public void Edit(EditPostDto postDto)
        {
            var post = _mapper.Map<Posts>(postDto);
            post.UserId = _postRepository.Get(post.Id).UserId;
            _postRepository.Update(post);
        }

        public IEnumerable<PostDto> GetAll()
        {
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(_postRepository.GetAll());

            var user = _userRepository.GetAll();

            foreach(var post in postsDto)
            {
                post.CreatedBy = user.FirstOrDefault(x => x.Id.Equals(post.UserId)).Surname;
                
            }
            return postsDto;
        }

        public Task<IEnumerable<PostDto>> GetAllEditableAndDeletableByUser(string userEmail)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => userEmail == u.Email);

            var result = GetAll().Where(post => post.UserId == user.Id);

            return Task.FromResult(result);
        }

        public EditPostDto GetByPostId(string Id)
        {
            var post = _mapper.Map<EditPostDto>(_postRepository.GetAll().FirstOrDefault(p => Id.Equals(p.Id.ToString())));

            return post;
        }
    }
    #endregion public method
}
