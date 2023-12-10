using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.PostDtos
{
    public class PostApiDto
    {
        public PostDto Post { get; set; }
        public IList<string>? Tags { get; set; }
    }
}
