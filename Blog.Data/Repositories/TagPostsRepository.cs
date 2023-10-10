using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories
{
    public class TagPostsRepository : GenericRepository<TagPosts>, IGenericRepository<TagPosts>, ITagPostsRepository
    {
        public TagPostsRepository(BlogDbContext context) : base(context)
        {
        }
        public void AddTagsToPost(IList<TagPosts> tagPosts)
        {
            _context.AddRange(tagPosts);
            _context.SaveChanges();
        }
    }
}
