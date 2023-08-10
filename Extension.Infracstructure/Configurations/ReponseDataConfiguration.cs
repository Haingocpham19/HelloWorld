using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Extension.Domain.Entities;

namespace Extension.Domain.Configuration
{
    public class ReponseDataConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductId);
        }
    }
}
