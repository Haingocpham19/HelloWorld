using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Extension.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExtensionDbContext>
    {
        public ExtensionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ExtensionDbContext>();
            var connectionString = configuration.GetConnectionString("Extension");

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ExtensionDbContext(builder.Options);
        }
    }
}
