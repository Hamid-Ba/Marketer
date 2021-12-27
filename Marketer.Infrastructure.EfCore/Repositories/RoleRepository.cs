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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly MarketerContext _context;
        public RoleRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<RoleVM>> GetAll() => await _context.Role.Select(r => new RoleVM
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
        }).AsNoTracking().ToListAsync();

        public Role GetBy(long roleId) => _context.Role.Find(roleId);

        public async Task<EditRoleVM> GetDetailForEditBy(long id) 
        {
            var result = await _context.Role.Select(r => new EditRoleVM
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
            }).FirstOrDefaultAsync(r => r.Id == id);

            result.PermissionsId = await _context.RolePermissions.Where(r => r.RoleId == id).Select(r => r.PermissionId).ToArrayAsync();

            return result;
        } 
        
    }
}