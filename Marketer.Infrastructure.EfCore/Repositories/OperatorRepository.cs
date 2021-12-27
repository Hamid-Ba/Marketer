using Framework.Infrastructure;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class OperatorRepository : Repository<Operator>, IOperatorRepository
    {
        private readonly MarketerContext _context;

        public OperatorRepository(MarketerContext context) : base(context) => _context = context;
        
    }
}
