using Identity.Application.Dtos;

namespace Identity.IApplication;

public interface IRoleApplication
{
    Task AddAsync(RoleDto roleModel);
    Task UpdateAsync(RoleDto roleModel);
    Task DeleteAsync(int roleId);
    bool CheckCode(int comanyId,string code,int? roleId);
}