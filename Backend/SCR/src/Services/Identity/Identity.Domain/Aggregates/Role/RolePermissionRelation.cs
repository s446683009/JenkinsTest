using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Role
{
    /// <summary>
    /// 人分配角色
    /// </summary>
    public class RolePermissionRelation
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

    }
}
