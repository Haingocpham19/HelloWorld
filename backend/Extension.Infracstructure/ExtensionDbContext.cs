using Extension.Domain.Configuration;
using Extension.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Extension.Infracstructure
{
    public class ExtensionDbContext : DbContext
    {
        public ExtensionDbContext()
        {
            Products = Set<Product>();
            Currencies = Set<Currency>();
            SourcePages = Set<SourcePage>();
            ClientCards = Set<ClientCard>();
        }
        public ExtensionDbContext(DbContextOptions options) : base(options)
        {
            Products = Set<Product>();
            Currencies = Set<Currency>();
            SourcePages = Set<SourcePage>();
            ClientCards = Set<ClientCard>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppconfigConfiguration());
            //base.OnModelCreating(modelBuilder);

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Currency> Currencies { set; get; }
        public DbSet<SourcePage> SourcePages { set; get; }
        public DbSet<ClientCard> ClientCards { get; set; }
    }
}
