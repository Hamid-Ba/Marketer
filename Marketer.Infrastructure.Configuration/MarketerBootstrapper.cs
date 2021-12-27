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
            service.AddTransient<IOperatorRepository, OperatorRepository>();
            service.AddTransient<IPermissionRepository, PermissionRepository>();
            service.AddTransient<IRolePermissionRepository, RolePermissionRepository>();

            #endregion
        }
    }
}