using Marketer.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class VisitorMapping : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FullName).HasMaxLength(75).IsRequired();
            builder.Property(p => p.UniqueCode).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
