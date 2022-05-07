using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models.Api.request
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
