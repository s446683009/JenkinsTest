using Identity.Application.Dto;
using Identity.Application.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries
{
    public interface IUserQuery:IQuery
    {
        Task<UserProfileDto> GetProfileAsync(int userId);
        Task<IEnumerable<UserListDto>> GetUsers(UserSearchRequest userSearchRequest);
    }
}
