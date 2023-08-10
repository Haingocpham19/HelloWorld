using Extension.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Extension.Domain.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ExtensionDbContext DbContext;
        private ExtensionDbContext _dbContext;
        public DbFactory(ExtensionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ExtensionDbContext Init() => DbContext ?? (DbContext = _dbContext);

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
