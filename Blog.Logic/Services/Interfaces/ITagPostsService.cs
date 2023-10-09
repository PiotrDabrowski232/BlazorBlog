
using Blog.Logic.Dto.PostDtos;

namespace Blog.Logic.Services.Interfaces
{
    public interface ITagPostsService
    {
        public void AddTagsToPost(string tagsName, Guid postId);
    }
}
