using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dto.Requests
{
    public class SearchRequest
    {

       public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
