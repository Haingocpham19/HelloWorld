using Extension.Application.AppFactory;
using Extension.Domain.Abstractions;
using Extension.Domain.Infrastructure;
using Extension.Domain.Repositories;
using Extension.Infracstructure;
using Extension.Web.Configs.DiLaunchers.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Extension.Web.Configs.DiLaunchers.Implementations
{
    public class DebugDiLauncher : IDiLauncher
    {
        public void Run(IServiceCollection container)
        {
            RegisterService(container);
        }

        private void RegisterService(IServiceCollection container)
        {
            container.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            container.AddScoped<IDbFactory, DbFactory>();
            container.AddScoped<IUnitOfWork, UnitOfWork>();
            //factory
            container.AddScoped<IAppFactory, AppFactory>();
            //repository
            container.AddScoped<IClientCardRepository, ClientCardRepository>();
            container.AddScoped<ICurrencyRepository, CurrencyRepository>();
            container.AddScoped<IProductRepository, ProductRepository>();
            container.AddScoped<ISourcePageRepository, SourcePageRepository>();
            //service
            container.AddScoped<ExtensionDbContext>();
            container.AddTransient<DomainSdk>();
        }
    }
}
