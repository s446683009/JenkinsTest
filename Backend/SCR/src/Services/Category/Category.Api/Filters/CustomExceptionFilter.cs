using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Services.Common;

namespace Category.Api.Filters;

/// <summary>
/// 除异常filter，还有authorizationFilter,ActionFilter,ResultFilter,控制器默认实现了这些过滤器，包含ApiController
/// 
/// </summary>
public class CustomExceptionFilter:Attribute,IExceptionFilter
{
    private ILogger<CustomExceptionFilter> _logger;
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> loger)
    {
        _logger = loger;
    }

    public  void OnException(ExceptionContext context)
    {
     
        var exception = context.Exception;
        if (exception?.InnerException != null)
        {
            exception = exception.InnerException;
        }
        
       
        _logger.LogError(exception, exception.Message);
        
        var apiBaseResult = new ApiResult<bool>()
        {
            Code = ApiResultCode.Fail,
            Data = false,
            Message = exception?.Message
        };
        context.Result = new JsonResult(apiBaseResult);

        
    }
}