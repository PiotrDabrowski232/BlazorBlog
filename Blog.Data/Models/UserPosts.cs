using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class UserPosts
    {
        public Guid IdUser { get; set; }
        public virtual User users { get; set; }
        public Guid IdPost { get; set; }
        public virtual Posts posts { get; set; }

    }
}
