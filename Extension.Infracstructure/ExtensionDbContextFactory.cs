using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Extension.Infracstructure
{
    public class ExtensionDbContextFactory : IDesignTimeDbContextFactory<ExtensionDbContext>
    {
        public ExtensionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Extension");

            var optionsBuilder = new DbContextOptionsBuilder<ExtensionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ExtensionDbContext(optionsBuilder.Options);
        }
    }

}
