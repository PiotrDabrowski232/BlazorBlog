namespace Blog.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task Add(T entity);
        public T Get(Guid id);
        public void Update(T entity);
        public void Remove(T entity);
        public IEnumerable<T> GetAll();
    }
}
