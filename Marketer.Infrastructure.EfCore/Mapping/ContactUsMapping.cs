using Marketer.Domain.Entities.Extera;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class ContactUsMapping : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FullName).HasMaxLength(125).IsRequired();
            builder.Property(p => p.MobilePhone).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Message).HasMaxLength(500).IsRequired();
        }
    }
}