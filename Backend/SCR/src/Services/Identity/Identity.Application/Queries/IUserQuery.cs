
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Application.Dtos.Requests;

namespace Identity.Application.Queries
{
    public interface IUserQuery:IQuery
    {
        Task<UserProfileDto> GetProfileAsync(int userId);
        Task<PageResult<UserListDto>> GetUsersAsync(UserSearchRequest userSearchRequest);
    }
    
}
