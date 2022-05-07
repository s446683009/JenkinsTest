using Identity.Domain.Aggregates.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates
{
    public class Role : BaseEntity,IAggregateRoot
    {

        public Role(string name, int companyId) {
            this.CompanyId = companyId;
            this.Name = name;
        }
        public int RoleId { get; set; }
        public string Name { get; private set; }

        public int CompanyId { get; private set; }
   
        public virtual Company Company { get; private set; }
        public virtual ICollection<Permission> Permissions { get; private set; }
        public virtual ICollection<User> Users { get; private set; }

    }
}
