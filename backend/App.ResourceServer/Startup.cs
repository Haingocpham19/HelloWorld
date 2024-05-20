using Extension.Domain.Enum;
using Extension.Infrastructure;
using Extension.Web.Configs.DiLaunchers.Implementations;
using Extension.Web.Middlewares;
using Hangfire;
using Hangfire.MySql;
using JwtAuthenticationHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using System;
using System.Configuration;
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
            ConfigureHangfire(services);
            ConfigureEntityFramework(services);
            ConfigureCors(services);
            ConfigureSwagger(services);
            ConfigureDependencyInjection(services);
            ConfigureControllers(services);
            ConfigureAuthorization(services);
            ConfigureOpenApiDocument(services);
            ConfigureJwtAuthentication(services);
        }

        #region private configure services
        private void ConfigureHangfire(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Extension");
            var options = new MySqlStorageOptions
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
            services.AddHangfire(cfg => cfg.UseStorage(storage));
            services.AddHangfireServer();
        }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Extension");
            services.AddDbContext<ExtensionDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
            options =>
            {
                options.SchemaBehavior(MySqlSchemaBehavior.Ignore);
            }));
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Application API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://github.com/Haingocpham19/HelloWorld"),
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

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };
                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                var jwtSecurityRequirement = new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(jwtSecurityRequirement);
            });
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            var launcher = FactoryLauncher.CreateDiLauncher(LauncherType.Debug);
            launcher.Run(services);
        }


        private void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization();
        }

        private void ConfigureOpenApiDocument(IServiceCollection services)
        {
            services.AddOpenApiDocument();
        }

        private void ConfigureJwtAuthentication(IServiceCollection services)
        {
            string validIssuer = Configuration.GetSection("JwtSettings:Issuer").Value;
            string validAudience = Configuration.GetSection("JwtSettings:Audience").Value;

            services.ConfigureJwtAuthentication(validIssuer, validAudience);
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello World Appilcation API");
            });

            // Sử dụng CORS
            app.UseCors("AllowAll"); app.UseHttpsRedirection();

            // Access specific configuration values
            var issuerSigningKey = Configuration.GetSection("JwtSettings:SecurityKey").Value;
            string validIssuer = Configuration.GetSection("JwtSettings:Issuer").Value;
            string validAudience = Configuration.GetSection("JwtSettings:Audience").Value;

            // Thêm middleware kiểm tra access token vào pipeline
            app.UseAccessTokenMiddleware(issuerSigningKey, validIssuer, validAudience);

            app.UseHangfireDashboard("/dashboard");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
