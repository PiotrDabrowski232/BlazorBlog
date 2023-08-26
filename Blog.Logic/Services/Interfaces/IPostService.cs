using Blog.Data.Models;
using Blog.Logic.Dto.PostDtos;
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
        public void Edit(EditPostDto post);
        public void Delete(Guid id);
        public Task<IEnumerable<PostDto>> GetAllEditableAndDeletableByUser(string userEmail);

        public EditPostDto GetByPostId(string Id);
    }
}
