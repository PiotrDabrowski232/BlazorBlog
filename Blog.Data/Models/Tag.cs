
namespace Blog.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TagPosts> Posts { get; set; }
    }
}
