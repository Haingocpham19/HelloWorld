using AutoMapper;
using AutoMapper.Internal.Mappers;
using Extension.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Extension.Application.AppFactory
{
    public interface IAppFactory
    {
        //Repository & Service
        TProxyOrService GetProxyOrServiceDependency<TProxyOrService>();
        IBaseRepository<TPrimaryKey,TEntity> Repository<TPrimaryKey, TEntity>()
            where TEntity : class;
        TScopedService GetScopedService<TScopedService>();

        // external
        //IConfiguration Configuration { get; }
        IMapper ObjectMapper { get; }
    }
}
