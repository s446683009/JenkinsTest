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

        public Role(string name, int companyId) {
            this.CompanyId = companyId;
            this.Name = name;
        }
        public int RoleId { get; set; }
        public string Name { get; private set; }

        public int CompanyId { get; private set; }
   
        public virtual Company.Company Company { get; private set; }
      
        public virtual ICollection<User.User> Users { get; private set; }

    }
}
