using Marketer.Application;
using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.AI.Discounts;
using Marketer.Application.Contract.AI.Extera;
using Marketer.Application.Contract.AI.Orders;
using Marketer.Application.Contract.AI.Products;
using Marketer.Domain.RI.Account;
using Marketer.Domain.RI.Discounts;
using Marketer.Domain.RI.Extera;
using Marketer.Domain.RI.Orders;
using Marketer.Domain.RI.Products;
using Marketer.Infrastructure.EfCore;
using Marketer.Infrastructure.EfCore.Repositories;
using Marketer.Query.Commands;
using Marketer.Query.Queries.Brands;
using Marketer.Query.Queries.Categories;
using Marketer.Query.Queries.Orders;
using Marketer.Query.Queries.Products;
using Marketer.Query.Queries.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Infrastructure.Configuration
{
    public class MarketerBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region ConfigContext

            service.AddDbContext<MarketerContext>(o => o.UseSqlServer(connectionString));

            #endregion

            #region Account

            service.AddTransient<IRoleRepository, RoleRepository>();
            service.AddTransient<IRoleApplication, RoleApplication>();

            service.AddTransient<IVisitorRepository, VisitorRepository>();
            service.AddTransient<IVisitorApplication, VisitorApplication>();

            service.AddTransient<IOperatorRepository, OperatorRepository>();
            service.AddTransient<IOperatorApplication, OperatorApplication>();

            service.AddTransient<IPermissionRepository, PermissionRepository>();
            service.AddTransient<IPermissionApplication, PermissionApplication>();

            service.AddTransient<IRolePermissionRepository, RolePermissionRepository>();
            service.AddTransient<IRolePermissionApplication, RolePermissionApplication>();

            #endregion

            #region Products

            service.AddTransient<ICityRepository, CityRepository>();
            service.AddTransient<ICityApplication, CityApplication>();

            service.AddTransient<IBrandRepository, BrandRepository>();
            service.AddTransient<IBrandApplication, BrandApplication>();

            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<ICategoryApplication, CategoryApplication>();

            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductApplication, ProductApplication>();

            service.AddTransient<IMarketRepository, MarketRepository>();
            service.AddTransient<IMarketApplication, MarketApplication>();

            #endregion

            #region Discounts

            service.AddTransient<IDiscountRepository, DiscountRepository>();
            service.AddTransient<IDiscountApplication, DiscountApplication>();

            #endregion

            #region Extera

            service.AddTransient<ISettingRepository, SettingRepository>();
            service.AddTransient<ISettingApplication, SettingApplication>();

            service.AddTransient<IContactUsRepository, ContactUsRepository>();
            service.AddTransient<IContactUsApplication, ContactUsApplication>();

            #endregion

            #region Orders

            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderApplication, OrderApplication>();

            #endregion

            #region Queries

            service.AddTransient<IOrderQuery, OrderQuery>();
            service.AddTransient<IBrandQuery, BrandQuery>();
            service.AddTransient<IProductQuery, ProductQuery>();
            service.AddTransient<ISettingQuery, SettingQuery>();
            service.AddTransient<ICategoryQuery, CategoryQuery>();

            #endregion
        }
    }
}