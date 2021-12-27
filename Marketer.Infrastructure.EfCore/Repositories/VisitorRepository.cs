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
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        private readonly MarketerContext _context;

        public VisitorRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<VisitorVM>> GetAll() => await _context.Visitors.Select(v => new VisitorVM
        {
            Id = v.Id,
            FullName = v.FullName,
            UniqueCode = v.UniqueCode,
            Mobile = v.Mobile,
            IsBlock = v.IsBlock,
            PlacedOrderCount = v.PlacedOrderCount
        }).AsNoTracking().ToListAsync();

        public async Task<EditVisitorVM> GetDetailForEditBy(long id) => await _context.Visitors.Select(v => new EditVisitorVM
        {
            Id = v.Id,
            FullName = v.FullName,
            UniqueCode = v.UniqueCode,
            Mobile = v.Mobile,
        }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

    }
}