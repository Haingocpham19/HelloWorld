using AutoMapper;
using Extension.Domain.Repositories;

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

        Task<TResponse> SendCqrsRequest<TResponse>(MediatR.IRequest<TResponse> request);

    }
}
