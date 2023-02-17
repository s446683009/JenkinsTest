using System;
using System.Data;
using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Domain.Aggregates.Role;
using Identity.IApplication;

namespace Identity.Application;

public class RoleApplication:IRoleApplication
{
    private readonly IRoleRepository _repository;

    public RoleApplication(IRoleRepository roleRepository)
    {
        _repository = roleRepository;
    }

    public async Task AddAsync(RoleDto roleModel)
    {
        CheckModel(roleModel);

        var role = new Role(roleModel.Name,roleModel.CompanyId,roleModel.Code);
        role.ChangePermissions(roleModel.PermissionIds);
        await _repository.AddRoleAsync(role);
        await _repository.UnitOfWork.SaveEntitiesAsync();
        
    }

    private void CheckModel(RoleDto roleModel)
    {
        if (string.IsNullOrWhiteSpace(roleModel.Name) || string.IsNullOrWhiteSpace(roleModel.Code))
        {
            throw new ArgumentException("Name or Code is null");
        }

        if (roleModel.CompanyId==0)
        {
            throw new ArgumentException("please select right company");
        }

        if (CheckCode(roleModel.CompanyId, roleModel.Code, roleModel.RoleId))
        {
            throw new DuplicateNameException("code duplicate");
        }
    }

    public async Task UpdateAsync(RoleDto roleModel)
    {
        CheckModel(roleModel);
        var role =await _repository.FindRoleById(roleModel.RoleId);
        if (role == null)
        {
            throw new NullReferenceException("update role is null");
        }
        role.UpdateRole(roleModel.Name,roleModel.Code,roleModel.PermissionIds);
        await _repository.UnitOfWork.SaveEntitiesAsync();
        
    }

    public async Task DeleteAsync(int roleId)
    {
        var role =await _repository.FindRoleById(roleId);
        if (role == null)
        {
            throw new NullReferenceException("update role is null");
        }
        _repository.Delete(role);
        await _repository.UnitOfWork.SaveEntitiesAsync();
    }

    public bool CheckCode(int companyId, string code,int? roleId)
    {
        if(roleId.HasValue)
            return _repository.GetExpressionCount(t=>t.CompanyId==companyId&&t.Code!=code&&t.RoleId!=roleId)>0;
        else
        {
            return _repository.GetExpressionCount(t=>t.CompanyId==companyId&&t.Code!=code)>0;
        }
    }
}