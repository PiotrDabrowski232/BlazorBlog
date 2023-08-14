using Blog.Data.Models;
using Blog.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostDto> GetAll();
        public void Add(PostDto post);
        public PostDto Edit(PostDto post);
        public void Delete(Guid id);
    }
}
