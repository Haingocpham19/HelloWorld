using Extension.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Extension.Infrastructure
{
    public class ExtensionDbContextFactory : IDesignTimeDbContextFactory<ExtensionDbContext>
    {
        public ExtensionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("Extension");

            var builder = new DbContextOptionsBuilder<ExtensionDbContext>();
            builder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));

            return new ExtensionDbContext(builder.Options);
        }
    }
}
