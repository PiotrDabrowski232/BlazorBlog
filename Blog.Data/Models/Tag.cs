
namespace Blog.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TagPosts> Posts { get; set; }
    }
}
