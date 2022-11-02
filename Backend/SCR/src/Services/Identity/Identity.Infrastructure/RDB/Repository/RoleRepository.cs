using Identity.Domain.Aggregates.Role;
using Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
    }
}
