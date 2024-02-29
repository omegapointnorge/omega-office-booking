using System.Linq.Expressions;

namespace server.Repository
{
    public interface IRepository<T>
    {
        public Task AddAsync(T entity);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        public Task<IEnumerable<T>> GetAsync();

        public Task Delete(T entity);
        public Task SaveAsync();

    }
}