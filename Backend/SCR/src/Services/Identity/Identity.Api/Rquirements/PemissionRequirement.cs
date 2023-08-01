using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace Identity.Api.Rquirements
{
    public class PemissionRequirement:IAuthorizationRequirement

    {
        //添加策略后，在需要进行该策略认证的方法或者控制器上加上 [Authorize(Policy ="permission")]
        //在验证授权的handler 上需要对此策略进行验证，如果通过则使用context.Succeed();标记通过该策略授权
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
