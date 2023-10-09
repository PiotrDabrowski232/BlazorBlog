using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Dto.PostDtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }

        public Guid UserId { get; set; }  
        public string CreatedBy { get; set; }

        public string? TagName {  get; set; }
    }
}
