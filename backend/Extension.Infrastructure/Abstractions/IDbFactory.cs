using Extension.Infrastructure;

namespace Extension.Domain.Abstractions
{
    public interface IDbFactory : IDisposable
    {
        public ExtensionDbContext Init();
    }
}
