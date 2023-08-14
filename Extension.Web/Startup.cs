using Extension.Domain.Abstractions;
using Extension.Domain.Infrastructure;
using Extension.Domain.Repositories;
using Extension.Infracstructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PCS.Extension.SaveJsonFile;
using PCS.Extension.Services;
using PCS.Extension.Services.implement;
using PCS.Extension.Services.interfaces;
using System;

namespace PCS.Extension
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Extension");
            services.AddDbContext<ExtensionDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddTransient<IResponseDataRepository, ResponseDataRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //repository
            services.AddScoped<IClientCardRepository, ClientCardRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISourcePageRepository, SourcePageRepository>();
            //service
            services.AddScoped<IClientCardService, ClientCardService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISourcePageService, SourcePageService>();
            services.AddScoped<ExtensionDbContext>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ChromeExtension API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD Pattern Example API");

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    try
            //    {
            //        ExtensionDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ExtensionDbContext>();
            //        DbInitializer.SeedData(dbContext).Wait();
            //    }
            //    catch (Exception)
            //    {
            //        throw new Exception();
            //    }
            //    finally { serviceScope.Dispose(); }
            //};
        }
    }
}
