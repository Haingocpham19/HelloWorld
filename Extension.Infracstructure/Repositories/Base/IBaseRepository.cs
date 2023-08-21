using System.Linq.Expressions;

namespace Extension.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        TEntity? GetById(object Id);
        IList<TEntity> GetAll();
        IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity,long>> predicate);
    }
}
