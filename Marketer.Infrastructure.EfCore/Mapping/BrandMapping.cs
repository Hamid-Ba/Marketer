using Marketer.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class BrandMapping : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.KeyWords).HasMaxLength(100);
            builder.Property(c => c.MetaDescription).HasMaxLength(500);
            builder.Property(c => c.Slug).HasMaxLength(150).IsRequired();

            builder.HasMany(p => p.Products)
                .WithOne(b => b.Brand)
                .HasForeignKey(f => f.BrandId);
        }
    }
}
