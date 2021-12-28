using Marketer.Domain.Entities.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class DiscountMapping : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Reason).HasMaxLength(500);
        }
    }
}
