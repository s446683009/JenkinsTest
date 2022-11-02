using System.Threading.Tasks;
using Identity.Domain.SeedWork;

namespace Identity.Domain.Aggregates.Permission;

public interface IPermissionRepository:IRepository<Permission>
{
    public Task<Permission> FindAsync(long id);
    public Task DeleteAsync(Permission entity);
    public Task UpdateAsync(Permission entity);
    public Task AddAsync(Permission entity);
    public Task<Permission> FindAsync(string name);
}