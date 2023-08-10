using Extension.Infracstructure;

namespace Extension.Domain.Abstractions
{
    public interface IDbFactory : IDisposable
    {
        ExtensionDbContext Init();
    }
}
