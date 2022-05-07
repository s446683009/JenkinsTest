
using Identity.Api.Handlers;
using Identity.Api.Models.Api;
using Identity.Api.Models.Api.request;
using Identity.Api.Models.Configs;
using Identity.Application;
using Identity.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Api.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IdentityApplication _identityApp;
        private JwtSetting _jwtSetting;
        public  AccountController(IdentityApplication identityApplication, JwtSetting jwtSetting)
        {
            _identityApp = identityApplication;
            _jwtSetting = jwtSetting;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ApiResult<string>> Login(LoginRequest loginRequest) {
            if (string.IsNullOrWhiteSpace(loginRequest.userName) || string.IsNullOrWhiteSpace(loginRequest.password)) {
                ApiResult<string>.Error("username or password can not be empty");
            }

            
            var user=await _identityApp.UserLoginAsync(loginRequest.userName,loginRequest.password);
            var securityKey = _jwtSetting.Secret;

            var claims = new List<Claim>() {
                new Claim("userName",user.Account),
                new Claim("id",user.UserId.ToString())
            };

            var token=TokenHelper.CreateToken(claims, securityKey);


            return ApiResult<string>.Success(token);


        }
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                await _identityApp.CreateUserAsync(registerRequest);

                return ApiResult<bool>.Success(true);
            } catch (Exception e) {

                return ApiResult<bool>.Error(e.Message);
            }
         
        }
       
    }
}
