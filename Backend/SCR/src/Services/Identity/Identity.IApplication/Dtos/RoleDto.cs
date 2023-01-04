using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class RoleDto
    {
        public RoleDto()
        {
            PermissionIds = new List<int>().ToArray();
            Permissions = new List<string>().ToArray();
        }

        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public string Code { get; set; }
        public int[] PermissionIds { get; set; }
        public string[] Permissions { get; set; }
    }

    


}
