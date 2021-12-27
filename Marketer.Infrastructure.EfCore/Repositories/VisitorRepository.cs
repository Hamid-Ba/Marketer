using Framework.Infrastructure;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        private readonly MarketerContext _context;

        public VisitorRepository(MarketerContext context) : base(context) => _context = context;
        
    }
}