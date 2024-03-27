using AutoMapper;
using Extension.Application.AppFactory;
using Extension.Domain.Abstractions;
using Extension.Domain.Enum;
using Extension.Domain.Infrastructure;
using Extension.Domain.Repositories;
using Extension.Infrastructure;
using Extension.Services;
using Extension.Web.Configs.DiLaunchers.Infrastructure;
using Extension.Web.Configs.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Extension.Web.Configs.DiLaunchers.Implementations
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IClientCardRepository, ClientCardRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISourcePageRepository, SourcePageRepository>();
        }

        public static void RegisterFactories(this IServiceCollection services)
        {
                       // AutoMapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAppFactory, AppFactory>();
            services.AddScoped<IDbFactory, DbFactory>();
        }

        public static void RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(CrudBaseService<,,,,>));
        }

        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddScoped<ExtensionDbContext>();
        }

        public static void RegisterOtherDependencies(this IServiceCollection services)
        {
            services.AddTransient<DomainSdk>();
        }
    }

    public class DebugDiLauncher : IDiLauncher
    {
        public void Run(IServiceCollection container)
        {
            container.RegisterRepositories();
            container.RegisterFactories();
            container.RegisterUnitOfWork();
            container.RegisterServices();
            container.RegisterDbContext();
            container.RegisterOtherDependencies();
        }
    }
}
