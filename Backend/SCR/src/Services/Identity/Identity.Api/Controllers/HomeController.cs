using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Models.Configs;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Api.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly JwtSetting _jwtsetting;

        public HomeController(ILogger<HomeController> logger,JwtSetting jwtSetting)
        {
            _jwtsetting = jwtSetting;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string tokenStr = string.Empty;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsetting.Secret));
            if (!HttpContext.AuthenticateAsync().Result.Succeeded) {
                //必须使用有参构造函数
                //用户登录需要一个主身份（公民）
                var indentity = new ClaimsIdentity("main");
                //身份内容包含 名字，email,角色
                indentity.AddClaim(new Claim("UserId","01"));
                indentity.AddClaim(new Claim(ClaimTypes.Name, "Solo"));
                indentity.AddClaim(new Claim(ClaimTypes.Email, "446683009@qq.com"));
                indentity.AddClaim(new Claim(ClaimTypes.Role, "System"));
                indentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
 
                //次要身份可以是多个（学生）
                var subIndentity = new ClaimsIdentity("sub");
                subIndentity.AddClaim(new Claim("studentNo", "20150817001"));//学号
                subIndentity.AddClaim(new Claim("className", "计算机应用技术班"));//学号

                //创建以indentity为主身份的用户信息
                var principal = new ClaimsPrincipal(indentity);
                principal.AddIdentity(subIndentity);
                //以Cookies的方式登录 
                //同一个用户不同浏览器登录如何解决？貌似不用解决，权限信息不用对应每次对话
                //HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal).Wait();
                // 结束

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = indentity,//创建声明信息
                    Issuer = _jwtsetting.Issuer,//Jwt token 的签发者
                    Audience =_jwtsetting.Audience,//Jwt token 的接收者
                    Expires = DateTime.Now.AddMinutes(_jwtsetting.ExpirationInMinutes),//过期时间
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenStr = tokenHandler.WriteToken(token);


            }
            ViewBag.token = tokenStr;
            return View();

        }
        [Authorize()]
        //[Authorize(policy:"permission")]
        public IActionResult Privacy()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles ="Admin")]
        [Authorize("")]
        public IActionResult Login() {

            return Content("登录成功");
        }

       
    }
}
