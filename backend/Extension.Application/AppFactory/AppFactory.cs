using AutoMapper;
using Extension.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extension.Application.AppFactory
{
    public class AppFactory : IAppFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _services = new Dictionary<string, object>();

        public AppFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IConfiguration Configuration => LazyGetRequiredService(ref _configuration);
        private IConfiguration _configuration;

        public IMapper ObjectMapper => LazyGetRequiredService(ref _objectMapper);
        private IMapper _objectMapper;

        private IMediator _mediator;
        public IMediator Mediator => LazyGetRequiredService(ref _mediator);

        public TProxyOrService GetProxyOrServiceDependency<TProxyOrService>()
        {
            var proxyService = _serviceProvider.GetService<TProxyOrService>();
            if (proxyService == null)
            {
                throw new Exception("ProxyOrService is not registered");
            }
            return proxyService;
        }

        public IBaseRepository<TPrimaryKey, TEntity> Repository<TPrimaryKey, TEntity>()
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var key = entityType.Name;

            if (!_repositories.TryGetValue(key, out var repository))
            {
                repository = _serviceProvider.GetService<IBaseRepository<TPrimaryKey, TEntity>>();
                if (repository == null)
                {
                    throw new Exception($"IBaseRepository<{typeof(TPrimaryKey)}, {typeof(TEntity)}> is not registered");
                }
                _repositories[key] = repository;
            }
            return (IBaseRepository<TPrimaryKey, TEntity>)repository;
        }

        public TScopedService GetScopedService<TScopedService>()
        {
            var type = typeof(TScopedService);
            var key = type.Name;

            if (!_services.TryGetValue(key, out var service))
            {
                service = _serviceProvider.GetService(type);
                if (service == null)
                {
                    throw new Exception($"{type.Name} is not registered");
                }
                _services[key] = service;
            }
            return (TScopedService)service;
        }

        public async Task<TResponse> SendCqrsRequest<TResponse>(IRequest<TResponse> request)
        {
            return await Mediator.Send(request);
        }

        #region LazyGetRequiredService
        private T LazyGetRequiredService<T>(ref T reference)
        {
            if (reference != null)
            {
                return reference;
            }
            lock (this)
            {
                if (reference == null)
                {
                    reference = _serviceProvider.GetRequiredService<T>();
                }
            }
            return reference;
        }
        #endregion
    }
}
