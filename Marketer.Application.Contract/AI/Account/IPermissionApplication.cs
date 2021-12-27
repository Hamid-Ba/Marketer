using Marketer.Application.Contract.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Account
{
    public interface IPermissionApplication
    {
        Task<IEnumerable<PermissionVM>> GetAll();
    }
}
