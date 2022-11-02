namespace Identity.IApplication;

public interface IPermissionApplication
{
    Task AddAsync(string name,int? parentId,IEnumerable<string> actions);
    Task UpdateAsync(int id,string name,int? parentId,IEnumerable<string> actions);
    Task DeleteAsync(long id);

}