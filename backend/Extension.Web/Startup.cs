using AutoMapper;
using Extension.Domain.Enum;
using Extension.Infracstructure;
using Extension.Web.Configs.DiLaunchers.Implementations;
using Extension.Web.Configs.DiLaunchers.Infrastructure;
using Extension.Web.Configs.Mapper;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PuppeteerSharp;
using System;
using System.ComponentModel;
using System.Transactions;

namespace Extension
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

            var options =  new MySqlStorageOptions
            {
                TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                QueuePollInterval = TimeSpan.FromSeconds(15),
                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                PrepareSchemaIfNecessary = true,
                DashboardJobListLimit = 50000,
                TablesPrefix = "_hangefire_"
            };

            var storage = new MySqlStorage(connectionString, options);

            services.AddHangfire(cfg =>
            {
                cfg.UseStorage(storage);
            });

            services.AddHangfireServer();

            services.AddDbContext<ExtensionDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
                            Name = "Hai Ngoc Pham",
                            Email = string.Empty,
                            Url = new Uri("https://facebook.com/Haingocpham19"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Hai Ngoc Pham",
                            Url = new Uri("https://github.com/Haingocpham19"),
                        }
                    });
                });
                //.AddSwaggerDocument();

            var launcher = FactoryLauncher.CreateDiLauncher(LauncherType.Debug);
            launcher.Run(services);

            services.AddControllers();

            services.AddOpenApiDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD Pattern Example API");

                });
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseHangfireDashboard("/dashboard");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
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
