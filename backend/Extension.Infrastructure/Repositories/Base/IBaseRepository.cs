using System.Linq.Expressions;

namespace Extension.Domain.Repositories
{
    public interface IBaseRepository<TPrimaryKey, TEntity>
        where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TPrimaryKey> DeleteAsync(TPrimaryKey entity);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        IQueryable<TEntity> GetAll();
        IAsyncEnumerable<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
