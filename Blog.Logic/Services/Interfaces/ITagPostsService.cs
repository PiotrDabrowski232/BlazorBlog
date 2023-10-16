namespace Blog.Logic.Services.Interfaces
{
    public interface ITagPostsService
    {
        public void AddTagsToPost(IList<string> tagsName, Guid postId);

        public IEnumerable<Guid> GetPostsByTagsName(IList<string> tagsName);
    }
}
