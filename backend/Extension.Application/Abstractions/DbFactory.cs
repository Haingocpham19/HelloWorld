using Extension.Domain.Abstractions;
using Extension.Infrastructure;

namespace Extension.Domain.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        public DbFactory(ExtensionDbContext? dbContext)
        {
            _dbContext = dbContext;
        }

        private ExtensionDbContext DbContext;
        static ExtensionDbContext? _dbContext;

        ExtensionDbContext IDbFactory.Init() => _dbContext ??= DbContext;

        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
