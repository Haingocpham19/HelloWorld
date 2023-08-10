using Extension.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
