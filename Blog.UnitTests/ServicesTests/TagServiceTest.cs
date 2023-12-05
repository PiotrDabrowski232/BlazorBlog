using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services;
using Blog.Logic.Services.Interfaces;
using Moq;

namespace Blog.UnitTests.ServicesTests
{
    public class TagServiceTest
    {
        private readonly Mock<ITagRepository> _tagRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly ITagService _tagService;

        public TagServiceTest()
        {
            _tagRepository = new Mock<ITagRepository>();
            _mapper = new Mock<IMapper>();
            _tagService = new TagService(_tagRepository.Object, _mapper.Object);
        }


        [Fact]
        public void Add_Returned_IenumerableOfTags()
        {
            //Arrange
            var tagNames = new List<string> { "Nazwa", "Nazwa1", "Nazwa2" };

            var tags = new List<Tag>()
            {
                new Tag(){ Name = "Nazwa"},
                new Tag(){ Name = "Nazwa1"},
                new Tag(){ Name = "Nazwa2"},

            };


            _tagRepository.Setup(t => t.GetAll()).Returns(tags);

            var tagsToAdd = tagNames
        .Select(t => tags.FirstOrDefault(tag => tag.Name == t) ?? new Tag { Name = t })
        .ToList();

            _tagRepository.Setup(t => t.Add(tagsToAdd));

            //Act
            var result = _tagService.Add(tagNames);

            //Assert

            Assert.Equal(result, tagsToAdd);

            _tagRepository.Verify(t => t.GetAll(), Times.Once);
            _tagRepository.Verify(t => t.Add(tagsToAdd), Times.Once);
        }
    }
}
