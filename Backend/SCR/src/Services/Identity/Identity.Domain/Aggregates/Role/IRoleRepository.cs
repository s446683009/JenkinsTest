using Identity.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Role
{
    public interface IRoleRepository:IRepository<Role>
    {
         Task<IList<Role>> GetRolesByIdsAsync(IList<int> roleIds);
         Task<Role> FindRoleById(int roleId);
         Task AddRoleAsync(Role role);
         int GetExpressionCount(Expression<Func<Role, bool>> expression);
         void Delete(Role role);
    }
}
