using Framework.Application;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Account
{
    public interface IRolePermissionApplication
    {
        Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId);
    }
}