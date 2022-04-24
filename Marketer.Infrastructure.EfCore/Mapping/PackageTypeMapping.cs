using Marketer.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class PackageTypeMapping : IEntityTypeConfiguration<PackageType>
	{
        public void Configure(EntityTypeBuilder<PackageType> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(125).IsRequired();

            builder.HasMany(p => p.Products)
                .WithOne(p => p.PackageType)
                .HasForeignKey(f => f.PackageTypeId);
        }
    }
}