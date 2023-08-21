using Extension.Domain.Abstractions;
using Extension.Infracstructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Extension.Domain.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbFactory _dbFactory;

        public BaseRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            _dbFactory = dbFactory;
            _unitOfWork = unitOfWork;
            _dbSet = GetDbSet();
        }

        public DbSet<TEntity> GetDbSet()
        {
            return _dbFactory.Init().Set<TEntity>();
        }

        public int Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _unitOfWork.Commit();
        }

        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _unitOfWork.Commit();
        }

        public TEntity? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public IList<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, long>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
