using Framework.Infrastructure;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private readonly MarketerContext _context;
        public RolePermissionRepository(MarketerContext context) : base(context) => _context = context;
    }
}
