﻿using Framework.Application;
using Marketer.Application.Contract.AI.Account;
using Marketer.Domain.Entities.Account;
using Marketer.Infrastructure.EfCore.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class RolePermissionApplication : IRolePermissionApplication
    {
        private readonly RolePermissionRepository _rolePermissionRepository;

        public RolePermissionApplication(RolePermissionRepository rolePermissionRepository) => _rolePermissionRepository = rolePermissionRepository;

        public async Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId)
        {
            OperationResult result = new();

            var perviousPermissions = await _rolePermissionRepository.GetAllEntitiesAsync();

            if (permissionsId != null && permissionsId.Count() > 0)
            {
                foreach (var permission in perviousPermissions) if (permission.RoleId == roleId) _rolePermissionRepository.DeleteEntity(permission);

                foreach (var permissionId in permissionsId)
                {
                    if (permissionId == 0) continue;
                    var rolePermission = new RolePermission(roleId, permissionId);
                    await _rolePermissionRepository.AddEntityAsync(rolePermission);
                }

                await _rolePermissionRepository.SaveChangesAsync();
            }

            return result.Succeeded();
        }
    }
}