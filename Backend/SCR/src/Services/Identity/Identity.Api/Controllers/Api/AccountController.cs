using Identity.Api.Models.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [Route("login")]
        public ApiResult<string> Login(string username,string password) {


            return ApiResult<string>.Success("");
        }



    }
}
