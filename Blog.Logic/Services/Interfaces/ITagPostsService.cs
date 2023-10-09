﻿
using Blog.Logic.Dto.PostDtos;

namespace Blog.Logic.Services.Interfaces
{
    public interface ITagPostsService
    {
        public void AddTagsToPost(IList<string> tagsName, Guid postId);
    }
}
