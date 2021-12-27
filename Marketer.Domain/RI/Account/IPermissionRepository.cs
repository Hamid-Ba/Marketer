using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.Entities.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Account
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<IEnumerable<PermissionVM>> GetAll();
    }
}
