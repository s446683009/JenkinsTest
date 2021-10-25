using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCR.Web.Rquirements;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SCR.Web.AuthorizeHanders
{
    public class PermissionAuthorizationHandler : IAuthorizationHandler
    {


        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            var descriptor = filterContext?.ActionDescriptor as ControllerActionDescriptor;
            string permission = string.Empty;

            if (descriptor == null)
            {
                var en = context.Resource as Endpoint;
            
                descriptor=en.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            if (descriptor == null)
                return;
                permission =
                   $"{descriptor.ControllerTypeInfo.Namespace}.{descriptor.ControllerTypeInfo.Name}.{descriptor.MethodInfo.Name}";
                context.Succeed(new PemissionRequirement(permission));

            //如果已经登录
            if (context.HasSucceeded)
            {
                //过期需要吗？
                //验证下权限
 
            }

            else
            {
                context.Fail();
            }
            
        }
    }
}
