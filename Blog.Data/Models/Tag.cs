
namespace Blog.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TagPosts> Posts { get; set; }
    }
}
