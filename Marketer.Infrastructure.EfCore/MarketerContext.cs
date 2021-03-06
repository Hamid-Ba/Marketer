using Marketer.Domain.Entities.Account;
using Marketer.Domain.Entities.Discounts;
using Marketer.Domain.Entities.Extera;
using Marketer.Domain.Entities.Orders;
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

            modelBuilder.Entity<City>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Market>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Visitor>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Discount>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Operator>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<PackageType>().HasQueryFilter(u => !u.IsDelete);
        }

        #region Account

        public DbSet<Role> Role { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Products

        public DbSet<City> Cities { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }

        #endregion

        #region Discount

        public DbSet<Discount> Discounts { get; set; }

        #endregion

        #region Extera

        public DbSet<Setting> Settings { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        #endregion

        #region Orders

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        #endregion
    }
}