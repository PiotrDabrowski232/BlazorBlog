using AutoMapper;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class TagPostsService : ITagPostsService
    {
        private readonly ITagPostsRepository _tagPostsRepository;
        private readonly IMapper _mapper;
        public TagPostsService(ITagPostsRepository tagPostsService, IMapper mapper)
        {
            _mapper = mapper;
            _tagPostsRepository = tagPostsService;
        }

        #region private methods

        #endregion private methods

        #region public methods
        public void AddTagsToPost(string tagsName, Guid postId)
        {
            var separateTags = tagsName.Split(' ');
        }
        #endregion public methods
    }
}
