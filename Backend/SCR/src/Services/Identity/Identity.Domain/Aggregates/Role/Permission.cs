using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates
{
    public class Permission:BaseEntity
    {
        public int PermissionId { get;  set; }
        public string Name { get; private set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<PermissionAction> Actions { get; private set; }

    }
    public class PermissionAction {
       public int PermissionId { get; set; }
       public string Action { get; set; }
    }
}
