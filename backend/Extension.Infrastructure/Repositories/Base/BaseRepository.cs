using Extension.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Extension.Domain.Repositories
{
    public class BaseRepository<TPrimaryKey, TEntity> : IBaseRepository<TPrimaryKey, TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            if (dbFactory == null)
                throw new ArgumentNullException(nameof(dbFactory));

            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
            _dbSet = dbFactory.Init().Set<TEntity>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<TPrimaryKey> DeleteAsync(TPrimaryKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _unitOfWork.CommitAsync();
            }
            return id;
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            // Get the primary key properties
            var keyProperties = typeof(TEntity)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)))
                .ToList();

            // Ensure the entity has a key defined
            if (keyProperties.Count == 0)
            {
                throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} does not have a primary key defined.");
            }

            // Construct the expression for the primary key
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, keyProperties[0].Name);
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            // Execute the query to find the entity by its primary key
            return await _dbSet.FirstOrDefaultAsync(lambda);
        }

        public IQueryable<TEntity> GetAllEntities()
        {
            return _dbSet.AsQueryable<TEntity>(); // This will return an IQueryable
        }

        public async IAsyncEnumerable<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // Implement async version of Find using IQueryable and AsAsyncEnumerable
            await foreach (var entity in _dbSet.Where(predicate).AsAsyncEnumerable())
            {
                yield return entity;
            }
        }
    }
}
