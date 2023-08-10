using Extension.Domain.Abstractions;
using Extension.Infracstructure;
using Microsoft.EntityFrameworkCore;

namespace Extension.Domain.Repositories
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        public readonly DbSet<Entity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbFactory _dbFactory;

        public BaseRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            _dbFactory = dbFactory;
            _unitOfWork = unitOfWork;
            _dbSet = GetDbSet();
        }

        public DbSet<Entity> GetDbSet()
        {
            return _dbFactory.Init().Set<Entity>();
        }

        public int Delete(Entity entity)
        {
            _dbSet.Remove(entity);
            return _unitOfWork.Commit();
        }

        public Entity Insert(Entity entity)
        {
            _dbSet.Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public int Update(Entity entity)
        {
            _dbSet.Update(entity);
            return _unitOfWork.Commit();
        }

        public Entity? GetById(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
