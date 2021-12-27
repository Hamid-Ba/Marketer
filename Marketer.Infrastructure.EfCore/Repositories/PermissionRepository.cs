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
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private readonly MarketerContext _context;
        public PermissionRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<PermissionVM>> GetAll() => await _context.Permissions.Select(p => new PermissionVM
        {
            Id = p.Id,
            ParentId = p.ParentId,
            Title = p.Title,
        }).AsNoTracking().ToListAsync();
        
    }
}