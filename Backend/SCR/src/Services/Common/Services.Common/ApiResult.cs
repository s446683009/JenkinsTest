using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Common
{
    public record ApiResult<T>
    {
        [JsonProperty(PropertyName = "code")]
        public ApiResultCode Code { get; set; }
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "message")]
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
