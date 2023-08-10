using Extension.Domain.Abstractions;
using Extension.Infracstructure;

namespace Extension.Domain.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        protected ExtensionDbContext? DbContext;
        private readonly ExtensionDbContext _dbContext;
        public DbFactory(ExtensionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ExtensionDbContext Init() => DbContext ??= _dbContext;
        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
