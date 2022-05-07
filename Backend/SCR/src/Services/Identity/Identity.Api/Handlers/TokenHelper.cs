using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Handlers
{
    public static class TokenHelper
    {
        public static string CreateToken(IEnumerable<Claim> cliams,string securityKey,int expireMi=14400) {



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "SCR",
                audience: "SCR",
                claims: cliams,
                expires: DateTime.Now.AddMinutes(expireMi),
                signingCredentials: creds);
           return  new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
