using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.RI.Account;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class OperatorRepository : Repository<Operator>, IOperatorRepository
    {
        private readonly MarketerContext _context;

        public OperatorRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<OperatorVM>> GetAll() => await _context.Operators.Include(r => r.Role).Select(o => new OperatorVM
        {
            Id = o.Id,
            FullName = o.FullName,
            Mobile = o.Mobile,
            RoleId = o.RoleId,
            RoleName = o.Role.Name
        }).AsNoTracking().ToListAsync();

        public async Task<Operator> GetBy(string mobile) => await _context.Operators.FirstOrDefaultAsync(o => o.Mobile == mobile);

        public async Task<EditOperatorVM> GetDetailForEditBy(long id) => await _context.Operators.Select(o => new EditOperatorVM
        {
            Id = o.Id,
            FullName = o.FullName,
            Mobile = o.Mobile,
            RoleId = o.RoleId,
        }).AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

    }
}