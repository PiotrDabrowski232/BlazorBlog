using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class TagPosts
    {
        public Guid PostId { get; set; }
        public virtual Posts Posts { get; set; }
        public Guid TagId { get; set; }
        public virtual Tag Tags { get; set; }
    }
}
