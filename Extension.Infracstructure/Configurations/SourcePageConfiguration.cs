using Extension.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Extension.Domain.Configuration
{
    public class SourcePageConfiguration : IEntityTypeConfiguration<SourcePage>
    {
        public void Configure(EntityTypeBuilder<SourcePage> builder)
        {
            builder.ToTable("SourcePages");
            builder.HasKey(x => x.SourcePageId);
        }
    }
}
