using Blog.Data.Models;

namespace Blog.Logic.Services.Interfaces
{
    public interface ITagService
    {
        public IEnumerable<Tag> Add(IList<string> tag);
        public IEnumerable<Tag> GetAllTags();
    }
}
