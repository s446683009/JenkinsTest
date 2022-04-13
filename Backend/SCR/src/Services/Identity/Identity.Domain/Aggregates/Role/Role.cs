using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates
{
    public class Role : BaseEntity,IAggregateRoot
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public int CompanyId { get; set; }
        public ICollection<string> Permissions { get; set; }


    }
}
