using AutoMapper;
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

        #endregion public methods
    }
}
