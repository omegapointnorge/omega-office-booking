using System.Linq.Expressions;

namespace server.Repository.Interface
{
    public interface IRepository<T>
    {
        public Task<T> AddAsync(T entity);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);
        public Task<IEnumerable<T>> GetAsync();

        public Task Delete(T entity);
        public Task SaveAsync();

    }
}
