
using Identity.Domain.Aggregates;
using Identity.Domain.Aggregates.User;
using Identity.Infrastructure.RDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Application.Dtos.Requests;

namespace Identity.Application.Queries
{
    public class UserQuery:IUserQuery
    {
        //CQRS
        //我们可以总结成一个原则，query service接受参数返回dto。query service中不允许存在domain object，如entity，value object, repository等
        //query service与command service不要相互调用
        private IdentityContext _context;
   
        public UserQuery(IdentityContext context) {

            _context = context;
        }
        public async Task<UserProfileDto> GetProfileAsync(int userId) {
            var user = await _context.Users.FindAsync(userId);
            var roleList=_context.Roles.ToList();
          


            if (user == null) {
                throw new NullReferenceException("找不到用户");
            }
            var result= ConvertUserProfileDto(user);
            if (user.Roles != null)
            {
                result.Roles = user.Roles.Select(t=>new RoleDto() { 
                    RoleId=t.RoleId,
                    Name=roleList.FirstOrDefault(c=>c.RoleId==t.RoleId)?.Name
                });
            }

         

            return result;
        }

        public Task<IEnumerable<UserListDto>> GetUsers(UserSearchRequest userSearchRequest)
        {
            var userQuery = _context.Users.Where(t => !t.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(userSearchRequest.UserName)) {
                userQuery = userQuery.Where(t => t.Account.Contains(userSearchRequest.UserName));
            }

            if (userSearchRequest.CompanyId.HasValue) {
                userQuery = userQuery.Where(t => t.Companies.Any(c => c.CompanyId == userSearchRequest.CompanyId));
            }

            return Task.FromResult(new List<UserListDto>().AsEnumerable());


        }

        private UserProfileDto ConvertUserProfileDto(User user) {
            return new UserProfileDto()
            {
                Account=user.Account,
                Avatar=user.Avatar,
                Description=user.Descrption,
                Email=user.Email,
                UserId=user.UserId,

            };
        
        }
    }
}
