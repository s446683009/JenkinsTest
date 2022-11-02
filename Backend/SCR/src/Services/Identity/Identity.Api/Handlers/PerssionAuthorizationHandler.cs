using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Rquirements;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;

namespace Identity.Api.Handers
{
    public class PermissionAuthorizationHandler : IAuthorizationHandler
    {

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            var descriptor = filterContext?.ActionDescriptor as ControllerActionDescriptor;
            var permission = string.Empty;
           
            if (descriptor == null)
            {
                var ht = context.Resource as HttpContext;
                var en = ht.GetEndpoint();
            
                descriptor=en.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            if (descriptor == null)
                return;
            permission =
                   $"{descriptor.ControllerTypeInfo.Namespace}.{descriptor.ControllerTypeInfo.Name}.{descriptor.MethodInfo.Name}";
               

            //如果已经登录
            if (context.HasSucceeded)
            {
                //过期需要吗？
                //验证下权限
                context.Succeed(new PemissionRequirement(permission));
            }

            else
            {
                context.Fail();
            }
            await Task.FromResult(1);
        }
    }
}
