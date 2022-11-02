using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.User
{
    public class UserRoleRelation
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
