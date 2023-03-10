using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos.Requests
{
    public class UserSearchRequest:PageSearchRequest
    {

        public string UserName { get; set; }

        public int? CompanyId { get; set; }
  
    }
}
