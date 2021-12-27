using Framework.Infrastructure;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private readonly MarketerContext _context;
        public PermissionRepository(MarketerContext context) : base(context) => _context = context;
        
    }
}
