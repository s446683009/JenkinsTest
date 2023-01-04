using Identity.Domain.Aggregates.User;
using Identity.Domain.Aggregates.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Role
{
    /// <summary>
    /// 为什么是独立聚合   可以脱离其他聚合根独立存在
    /// </summary>
    public class Role : BaseEntity,IAggregateRoot
    {
        public Role(string name, int companyId,string code) {
            this.CompanyId = companyId;
            this.Name = name;
            this.Code = code;
        }
        public int RoleId { get; set; }
        public string Code { get;private set; }
        public string Name { get; private set; }

        public int CompanyId { get; private set; }
   
        public virtual Company.Company Company { get; private set; }
      
        public virtual ICollection<User.User> Users { get; private set; }
        public virtual ICollection<RolePermissionRelation> PermissionRelations { get; private set; }

        public void UpdateRole(string code,string name,int[] permissionIds)
        {
            if(!string.IsNullOrWhiteSpace(code))
                this.Code = code;
            if (!string.IsNullOrWhiteSpace(name))
                this.Name = name;
            if (permissionIds!=null)
            {
                ChangePermissions(permissionIds);
            }
        }

        public void ChangePermissions(int[] permissionIds)
        {
            this.PermissionRelations.Clear();
            this.PermissionRelations = permissionIds.Select(t => new RolePermissionRelation()
            {
                PermissionId = t
            }).ToList();
        }
    }
}
