using Marketer.Domain.Entities.Extera;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class SettingMapping : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(p => p.Emails).HasMaxLength(500);
            builder.Property(p => p.Mobiles).HasMaxLength(47);
            builder.Property(p => p.Text).HasMaxLength(1000);
        }
    }
}