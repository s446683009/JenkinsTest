using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos.Requests
{
    public class SearchRequest
    {

       public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
