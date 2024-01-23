using Microsoft.EntityFrameworkCore;
using server.Context;
using System.Linq.Expressions;

namespace server.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        internal OfficeDbContext dbContext;
        internal DbSet<T> dbSet;

        public Repository(OfficeDbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }
        /// <summary>
        /// Add an entity to database
        /// </summary>
        /// <param name="entity">Entity to store in the database</param>
        /// <returns>Newly created entity</returns>
        public async Task AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
        }

        /// <summary>
        /// Get the entity based on the filter
        /// </summary>
        /// <param name="filter">pass the filter like id==1, etc</param>
        /// <returns>first matched entity</returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.FirstAsync(filter);
        }
        /// <summary>
        /// Get collection of entities
        /// </summary>
        /// <returns>collection of entities</returns>
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await dbSet.ToListAsync();
        }


        public async Task DeleteAndCommit(T entity)
        {
            dbContext.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}