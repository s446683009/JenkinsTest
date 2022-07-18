using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dto.Requests
{
    public class UserSearchRequest:SearchRequest
    {

        public string UserName { get; set; }

        public int? CompanyId { get; set; }
        public int Page { get; set; } = 1;

        public int Rows { get; set; } = 999;
    }
}
