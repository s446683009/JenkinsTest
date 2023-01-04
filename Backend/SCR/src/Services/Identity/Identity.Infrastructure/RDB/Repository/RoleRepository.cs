using Identity.Domain.Aggregates.Role;
using Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.RDB.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private IdentityContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public  RoleRepository(IdentityContext context){
            _context = context;
        }
        public async Task<IList<Role>> GetRolesByIdsAsync(IList<int> roleIds)
        {
          return  await _context.Roles.Where(t=>roleIds.Contains(t.RoleId)).ToListAsync();
        }

        public async Task<Role> FindRoleById(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public int GetExpressionCount(Expression<Func<Role,bool>> expression)
        {
            return _context.Roles.Count(expression);
        }

        public void Delete(Role role) => _context.Roles.Remove(role);

    }
}
