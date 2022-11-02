using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Aggregates.Permission;
using Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.RDB.Repository;

public class PermissionRepository:IPermissionRepository
{
    private readonly  IdentityContext _context;
    public IUnitOfWork UnitOfWork => _context;
    
    public PermissionRepository(IdentityContext context) {
        _context = context;
    }

    public async Task<Permission> FindAsync(long id)
    {
        return await _context.Set<Permission>().FindAsync(id);
    }

    public Task DeleteAsync(Permission entity)
    {
        _context.Set<Permission>().Remove(entity);
        return Task.FromResult(1);
    }

    public Task UpdateAsync(Permission entity)
    {
        _context.Set<Permission>().Update(entity);
        return Task.FromResult(1);
    }

    public async Task AddAsync(Permission entity)
    {
        await _context.Set<Permission>().AddAsync(entity);
       
    }

    public Task<Permission> FindAsync(string name)
    {
        return _context.Set<Permission>().Where(t => t.Name == name).FirstOrDefaultAsync();
    }
}