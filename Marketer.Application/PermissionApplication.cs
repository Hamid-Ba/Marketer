using Marketer.Application.Contract.AI.Account;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.RI.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class PermissionApplication : IPermissionApplication
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionApplication(IPermissionRepository permissionRepository) => _permissionRepository = permissionRepository;

        public async Task<IEnumerable<PermissionVM>> GetAll() => await _permissionRepository.GetAll();
        
    }
}