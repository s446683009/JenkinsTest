using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace SCR.Web.Rquirements
{
    public class PemissionRequirement:IAuthorizationRequirement

    {
        public PemissionRequirement() { }
        public PemissionRequirement(string name) {
            Name = name;
        }
        public PemissionRequirement(string name,string displayname) {
            Name = name;
            DisplayName = displayname;
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }


       
    }
}
