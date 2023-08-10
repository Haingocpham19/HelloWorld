using Extension.Domain.Context;
using System;

namespace Extension.Domain.Abstractions
{
    public interface IDbFactory : IDisposable
    {
        ExtensionDbContext Init();
    }
}
