using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class TagPostsService : ITagPostsService
    {
        private readonly ITagPostsRepository _tagPostsRepository;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly ITagRepository _tagRepository;

        public TagPostsService(ITagPostsRepository tagPostsService, IMapper mapper, ITagService tagService, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagPostsRepository = tagPostsService;
            _tagService = tagService;
            _tagRepository = tagRepository;
        }

        #region private methods

        #endregion private methods

        #region public methods
        public void AddTagsToPost(IList<string> tagsName, Guid postId)
        {
            var tagsId = _tagService.Add(tagsName).Select(t => t.Id);

            var tagPost = tagsId.Select(tagId => new TagPosts { TagId = tagId, PostId = postId }).ToList();

            _tagPostsRepository.AddTagsToPost(tagPost);
        }

        public IEnumerable<Guid> GetPostsByTagsName(IList<string> tagsName)
        {
            var tagsId = _tagRepository.GetAll()
                .Where(t => tagsName.Contains(t.Name))
                .Select(t => t.Id)
                .ToList();

            var postIds = _tagPostsRepository.GetAll()
                .Where(p => tagsId.Contains(p.TagId))
                .Select(p => p.PostId)
                .Distinct();

            return postIds;
        }
        #endregion public methods
    }
}
