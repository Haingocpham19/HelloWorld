using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Extension.Domain.Entities;

namespace Extension.Domain.Configuration
{
    public class ClientCardConfiguration : IEntityTypeConfiguration<ClientCard>
    {
        public void Configure(EntityTypeBuilder<ClientCard> builder)
        {
            builder.ToTable("ClientCards");
            builder.HasKey(x => x.ClientCardId);
        }
    }
}
