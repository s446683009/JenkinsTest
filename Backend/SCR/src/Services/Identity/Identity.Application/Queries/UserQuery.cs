using Identity.Application.Dto;
using Identity.Domain.Aggregates;
using Identity.Domain.Aggregates.Entity;
using Identity.Infrastructure.RDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (user == null) {
                throw new NullReferenceException("用户不存在");
            }
            return ConvertUserProfileDto(user);
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
