using Identity.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates
{
    public interface IRoleRepository:IRepository<Role>
    {
         Task<IList<Role>> GetRolesByIdsAsync(IList<int> roleIds);
         Task AddRoleAsync(Role role);
    }
}
