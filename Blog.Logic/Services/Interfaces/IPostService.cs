using Blog.Logic.Dto.PostDtos;

namespace Blog.Logic.Services.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostDto> GetAll();
        public void Add(PostDto post, IList<string>? tags);
        public void UpdatePost(PostDto post);
        public void Delete(Guid id);
        public Task<IEnumerable<PostDto>> GetAllEditableAndDeletableByUser(string userEmail);
        public T GetByPostId<T>(string Id) where T : class;
    }
}
