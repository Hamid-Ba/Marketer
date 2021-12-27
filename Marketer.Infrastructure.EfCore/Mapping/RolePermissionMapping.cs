using Marketer.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketer.Infrastructure.EfCore.Mapping
{
    public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(k => new { k.RoleId, k.PermissionId });

            builder.HasOne(r => r.Role)
                .WithMany(p => p.Permissions)
                .HasForeignKey(f => f.RoleId);

            builder.HasOne(r => r.Permission)
                .WithMany(p => p.Roles)
                .HasForeignKey(f => f.PermissionId);
        }
    }
}
