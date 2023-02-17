using Identity.Application.Dtos;

namespace Identity.IApplication;

public interface ICompanyApplication
{ 
    Task AddCompanyAsync(CompanyDto model);
    Task UpdateCompanyAsnyc(CompanyDto model);
    Task DeleteCompanyAsync(int id);
    Task UpDateCompanySettings(int companyId,IDictionary<string,string> settings);
    Task AddCompanySettings(int companyId, IDictionary<string, string> settings);
    Task DeleteCompanySettings(int companyId, IEnumerable<string> keys);
    
    
}