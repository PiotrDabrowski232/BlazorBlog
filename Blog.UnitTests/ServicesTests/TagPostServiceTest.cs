using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Blog.Logic.Services;
using Blog.Logic.Services.Interfaces;
using Microsoft.VisualBasic;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.ServicesTests
{
    public class TagPostServiceTest
    {
        private readonly Mock<ITagPostsRepository> _tagPostsRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITagService> _tagService;
        private readonly Mock<ITagRepository> _tagRepository;
        private readonly ITagPostsService _tagPostsService;

        public TagPostServiceTest()
        {
            _tagPostsRepository = new Mock<ITagPostsRepository>();
            _mapper = new Mock<IMapper>();
            _tagService = new Mock<ITagService>();
            _tagRepository  = new Mock<ITagRepository>();
            _tagPostsService = new TagPostsService(_tagPostsRepository.Object, _mapper.Object, _tagService.Object, _tagRepository.Object);
        }


        [Fact]
        public void AddTagsToPost_AddedTagsToConcretePost()
        {
            // Arrange
            var postId = Guid.NewGuid();

            var tagsName = new List<string> { "string1", "string2", "string3", "string4" };

            var tags = tagsName
                .Select(tagName => new Tag { Name = tagName, Id = Guid.NewGuid() })
                .ToList();

            var tagPosts = tags
                .Select(tag => new TagPosts { TagId = tag.Id, PostId = postId })
                .ToList();

            _tagService.Setup(t => t.Add(It.IsAny<IList<string>>())).Returns(tags);



            // Act
            _tagPostsService.AddTagsToPost(tagsName, postId);



            // Assert
            _tagService.Verify(t => t.Add(It.IsAny<IList<string>>()), Times.Once);

            _tagPostsRepository.Verify(t => t.AddTagsToPost(It.Is<List<TagPosts>>(tp => tp.Count == 4)), Times.Once);
        }


        [Fact]
        public void GetPostByTagsName_Returned_PostsWithConcreteTags()
        {
            //Arrange

            var tagsNames = new List<string> { "string1", "string2", "string3", "string4" };

            var tags = tagsNames.Select(tagName => new Tag { Name = tagName, }).ToList();

            var tagsId = tags.Select(tag => tag.Id).ToList();

            var postIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            _tagRepository.Setup(t => t.GetAll()).Returns(tags);

            var tagsPosts = postIds.Select(postId => new TagPosts { TagId = tagsId.First(), PostId = postId }).ToList();

            _tagPostsRepository.Setup(tp => tp.GetAll()).Returns(tagsPosts);

            //Act

            var result = _tagPostsService.GetPostsByTagsName(tagsNames);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(result.Count(), postIds.Count);
            Assert.True(postIds.All(result.Contains));

            _tagPostsRepository.Verify(tp => tp.GetAll(), Times.Once);

            _tagRepository.Verify(t => t.GetAll(), Times.Once);

        }

    }
}
