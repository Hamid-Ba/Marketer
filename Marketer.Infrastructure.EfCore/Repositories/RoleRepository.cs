using Framework.Infrastructure;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly MarketerContext _context;
        public RoleRepository(MarketerContext context) : base(context) => _context = context;
        
    }
}
