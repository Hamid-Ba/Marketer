using Marketer.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class MarketMapping : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(125).IsRequired();
            builder.Property(p => p.Owner).HasMaxLength(75).IsRequired();
            builder.Property(p => p.MobilePhone).HasMaxLength(11).IsRequired();

            builder.HasOne(v => v.Visitor)
                .WithMany(m => m.Markets)
                .HasForeignKey(v => v.VisitorId);
        }
    }
}
