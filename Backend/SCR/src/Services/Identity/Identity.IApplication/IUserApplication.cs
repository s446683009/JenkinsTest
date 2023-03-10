
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Application.Dtos.Requests;
using Identity.IApplication.Dtos.Requests;

namespace Identity.IApplication
{
    public interface IUserApplication
    {
        //comand
        Task<UserDto> UserLoginAsync(string userName,string password);

        Task CreateUserAsync(RegisterRequest registerRquest);
        
        //Task<bool> ChangeOldPassword(string oldPassword,string newPassword);
        //Task<bool> ChangePassword(string newPassword);
        
        //query
        Task<UserProfileDto> GetProfileAsync(int userId);
        Task<PageResult<UserListDto>> GetUsersAsync(UserSearchRequest request);

    }
}
