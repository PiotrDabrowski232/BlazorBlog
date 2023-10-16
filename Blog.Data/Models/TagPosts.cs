using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class TagPosts
    {
        public  Posts Post { get; set; }
        public Guid PostId { get; set; }
        public  Tag Tag { get; set; }
        public Guid TagId { get; set; }

    }
}
