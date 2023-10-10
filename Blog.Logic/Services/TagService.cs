using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services.Interfaces;

namespace Blog.Logic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagService, IMapper mapper)
        {
            _mapper = mapper;
            _tagRepository = tagService;
        }

        #region private methods

        #endregion private methods

        #region public methods
        public IEnumerable<Tag> GetAllTags()
        {
            return _tagRepository.GetAll();
        }

        public IEnumerable<Tag> Add(IList<string> tagNames)
        {
            var existingTags = _tagRepository.GetAll();

            var tagsToAdd = tagNames
                .Select(t => existingTags.FirstOrDefault(tag => tag.Name == t) ?? new Tag { Name = t })
                .ToList();

            _tagRepository.Add(tagsToAdd);
            return tagsToAdd;
        }
        #endregion public methods
    }
}
