namespace Marketer.Domain.Entities.Account
{
    public class RolePermission
    {
        public long RoleId { get; private set; }
        public long PermissionId { get; private set; }

        public Role Role { get; private set; }
        public Permission Permission { get; private set; }

        public RolePermission(long roleId, long permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
