using Blog.Data.Data;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories
{
    public class TagRepository : GenericRepository<Tag>, IGenericRepository<Tag>, ITagRepository
    {
        public TagRepository(BlogDbContext context) : base(context)
        {
        }

        public void Add(IList<Tag> tags)
        {
            var existingTagNames = _context.tag.Select(t => t.Name).ToList();
            var tagsToAdd = tags.Where(tag => !existingTagNames.Contains(tag.Name)).ToList();

            _context.tag.AddRange(tagsToAdd);
            _context.SaveChanges();
        }

    }
}
