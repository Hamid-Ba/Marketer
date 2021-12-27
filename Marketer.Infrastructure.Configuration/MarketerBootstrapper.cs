using Marketer.Application;
using Marketer.Application.Contract.AI.Account;
using Marketer.Domain.RI.Account;
using Marketer.Infrastructure.EfCore;
using Marketer.Infrastructure.EfCore.Repositories;
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
        }
    }
}