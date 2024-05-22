using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.Common
{
    public record ApiResult<T>
    {
      
        public ApiResultCode Code { get; set; }
  
        public T Data { get; set; }
   
        public string Message { get; set; }
        public static ApiResult<T> Success(T data, string message=null) {
            return new ApiResult<T>() { 
                Data=data,
                Code=ApiResultCode.Success,
                Message=message
            };
        }
        public static ApiResult<T> Error( string message)
        {
            return new ApiResult<T>()
            {
         
                Code = ApiResultCode.Fail,
                Message = message
            };
        }
    }
}
