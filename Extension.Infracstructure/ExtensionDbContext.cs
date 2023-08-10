using Microsoft.EntityFrameworkCore;
using Extension.Domain.Configuration;
using Extension.Domain.Entities;

namespace Extension.Infracstructure
{
    public class ExtensionDbContext : DbContext
    {
        public ExtensionDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppconfigConfiguration());
            //base.OnModelCreating(modelBuilder);

        }
        public DbSet<Products>? Products { set; get; }
        public DbSet<Currency>? Currencies { set; get; }
        public DbSet<SourcePage>? SourcePages { set; get; }
        public DbSet<ClientCard>? ClientCards { get; set; }
    }
}
