using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.Entities.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Account
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetBy(long roleId);
        Task<IEnumerable<RoleVM>> GetAll();
        Task<EditRoleVM> GetDetailForEditBy(long id);
    }
}
