using Extension.Domain.Configuration;
using Extension.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Extension.Infrastructure
{
    public class ExtensionDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExtensionDbContext(DbContextOptions<ExtensionDbContext> options) : base(options) { }

        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SourcePage> SourcePages { get; set; }
        public DbSet<ClientCard> ClientCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ClientCardConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new SourcePageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
