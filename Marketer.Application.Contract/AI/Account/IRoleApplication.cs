using Framework.Application;
using Marketer.Application.Contract.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Account
{
    public interface IRoleApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<RoleVM>> GetAll();
        Task<EditRoleVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditRoleVM command);
        Task<OperationResult> Create(CreateRoleVM command);
        bool IsRoleHasThePermission(long roleId, long permissionId);
    }
}
