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

    public class PageSearchRequest : SearchRequest
    {
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 999;
    }
}

   
