using System.Collections.Generic;

namespace Marketer.Domain.Entities.Account
{
    public class Permission 
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public long? ParentId { get; private set; }

        public List<RolePermission> Roles { get; private set; }
    }
}
