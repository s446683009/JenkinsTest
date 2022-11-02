using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class PermissionController:Controller
{
    [Route("actions")]
    public  async Task<IEnumerable<string>> GetActions()
    {
        var  assembly= Assembly.GetExecutingAssembly();
        var types=assembly.GetTypes().AsEnumerable();
        types= types.Where(t => t.CustomAttributes.Any(c => c.AttributeType == typeof(ApiControllerAttribute)));
        var methodsGp = types.Select(t => new
        {
            type=t,
            methods=t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        });
        var methods = methodsGp.SelectMany(t=>t.methods.Select(m=>
                $"{t.type.Namespace}.{t.type.Name}.{m.Name}"
            ));

        return await Task.FromResult(methods);
    }
    
}