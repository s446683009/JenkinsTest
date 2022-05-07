using Identity.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IdentityApplication
    {
        Task<UserDto> UserLoginAsync(string userName,string password);

        Task CreateUserAsync(RegisterRequest registerRquest);
    }
}
