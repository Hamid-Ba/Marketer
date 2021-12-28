using Marketer.Domain.Entities.Account;
using Marketer.Domain.Entities.Products;
using Marketer.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Marketer.Infrastructure.EfCore
{
    public class MarketerContext : DbContext
    {
        public MarketerContext(DbContextOptions<MarketerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var assembly = typeof(OperatorMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Market>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Visitor>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Operator>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDelete);
        }

        #region Account

        public DbSet<Role> Role { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Products

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion
    }
}