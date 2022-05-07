using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models.Api
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string Account { get; set; }
        public string  NickName{get;set;}
            
    }
}
