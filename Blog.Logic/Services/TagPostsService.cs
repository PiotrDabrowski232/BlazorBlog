﻿using AutoMapper;
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

        public TagPostsService(ITagPostsRepository tagPostsService, IMapper mapper, ITagService tagService)
        {
            _mapper = mapper;
            _tagPostsRepository = tagPostsService;
            _tagService = tagService;
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
        #endregion public methods
    }
}