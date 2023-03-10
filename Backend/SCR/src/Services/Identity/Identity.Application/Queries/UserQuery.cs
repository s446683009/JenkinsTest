
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
using Microsoft.EntityFrameworkCore;

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

        public async Task<PageResult<UserListDto>> GetUsersAsync(UserSearchRequest request)
        {
            var userQuery = _context.Users.Where(t => !t.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(request.UserName)) {
                userQuery = userQuery.Where(t => t.Account.Contains(request.UserName));
            }

            if (request.CompanyId.HasValue) {
                userQuery = userQuery.Where(t => t.Companies.Any(c => c.CompanyId == request.CompanyId));
            }

            var count = userQuery.Count();
            var userList = await userQuery.Skip((request.Page - 1) * request.Rows).Take(request.Rows).Select(t=>new UserListDto()
            {
                Description =t.Descrption,
                UserId = t.UserId,
                Email = t.Email,
                Account = t.Account,
                CreateDate = t.CreatedTime,
                Actived= t.IsActived,
                Avatar = t.Avatar

            }).ToListAsync();

            return new PageResult<UserListDto>()
            {
                List = userList,
                Total =count,
                Page = request.Page,
                Rows =request.Rows
            };
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
