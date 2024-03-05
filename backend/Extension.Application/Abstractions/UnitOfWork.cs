using Extension.Domain.Abstractions;
using Extension.Infrastructure;

namespace Extension.Domain.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private ExtensionDbContext? _dbContext = null;

        public ExtensionDbContext DbContext => _dbContext ??= _dbFactory.Init();

        public UnitOfWork(IDbFactory dbFactory) => _dbFactory = dbFactory;

        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
