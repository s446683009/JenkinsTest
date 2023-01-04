using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.IApplication;
using Identity.Domain.Aggregates.Permission;
namespace Identity.Application;

public class PermissionApplication:IPermissionApplication
{
    private readonly IPermissionRepository _permissionRepository;
    public PermissionApplication(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task AddAsync(string name,int? parentId, IEnumerable<string> actions)
    {
        var permission = new Permission(name,parentId??0); 
         await _permissionRepository.AddAsync(permission);
         await _permissionRepository.UnitOfWork.SaveEntitiesAsync();
        
    }

    public async Task UpdateAsync(int id,string name, int? parentId,IEnumerable<string> actions)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name),"the value can not be null or white space");
        }

        var permission = await _permissionRepository.FindAsync(id);
        
        permission.ChangeName(name);
        permission.ChangeParentId(parentId??0);
        permission.ChangePermission(actions);
       await _permissionRepository.UnitOfWork.SaveEntitiesAsync();

    }

    public async Task DeleteAsync(long id)
    {
        var permission = await _permissionRepository.FindAsync(id);
        
        await _permissionRepository.DeleteAsync(permission);
    }
    
    
}