

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Configurations;
using Identity.Api.Handlers;
using Identity.Api.Models;
using Identity.Api.Models.Api;
using Identity.Api.Models.Api.request;
using Identity.Application.Dtos;
using Identity.Application.Dtos.Requests;
using Identity.IApplication;
using Identity.IApplication.Dtos.Requests;
using Services.Common;
using System.Collections.Generic;
using Identity.Domain.Aggregates.User;

namespace Identity.Api.Controllers.Api
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserApplication _IUserApp;
        private JwtSetting _jwtSetting;
        private IEnumerable<IUserRepository> _userRepositories;
        public  AccountController(IUserApplication IUserApplication, JwtSetting jwtSetting,IEnumerable<IUserRepository> userRepositories)
        {
            _IUserApp = IUserApplication;
            _jwtSetting = jwtSetting;
            _userRepositories = userRepositories;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ApiResult<string>> Login(LoginRequest loginRequest) {
            if (string.IsNullOrWhiteSpace(loginRequest.userName) || string.IsNullOrWhiteSpace(loginRequest.password)) {
                ApiResult<string>.Error("username or password can not be empty");
            }
            var user=await _IUserApp.UserLoginAsync(loginRequest.userName,loginRequest.password);
            var securityKey = _jwtSetting.Secret;

            var claims = new List<Claim>() {
                new Claim(IdentityConst.userName,user.Account),
                new Claim(IdentityConst.userId,user.UserId.ToString())
            };

            var token=TokenHelper.CreateToken(claims, securityKey);
            

            return ApiResult<string>.Success(token);


        }
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(UserDto), 200)]
        //基于·这个策略的话需要单独对策略去认证
        //[Authorize(Policy ="permission")]
        [Authorize]
        
        public async Task<ApiResult<UserDto>> GetProfileAsync() {
            var userId = int.Parse(User.FindFirst(IdentityConst.userId).Value);
            var profile = await _IUserApp.GetProfileAsync(userId);
            return ApiResult<UserDto>.Success(profile);
        }

       

        [Route("register")]
        [HttpPost]
        [ProducesResponseType(typeof(bool),200)]
        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                await _IUserApp.CreateUserAsync(registerRequest);
                return ApiResult<bool>.Success(true);
            } catch (Exception e) {

                return ApiResult<bool>.Error(e.Message);
            }
         
        }


        [Route("users")]
        [HttpPost]
        [ProducesResponseType(typeof(PageResult<UserListDto>), 200)]
        public async Task<ApiResult<PageResult<UserListDto>>> GetUsersAsync(UserSearchRequest request)
        {
            var result= await _IUserApp.GetUsersAsync(request);
            return ApiResult<PageResult<UserListDto>>.Success(result);
        }

    }
}
