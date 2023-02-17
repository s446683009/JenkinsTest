using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Dtos;
using Identity.Domain.Aggregates.Company;
using Identity.IApplication;

namespace Identity.Application;

public class CompanyApplication:ICompanyApplication
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyApplication(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public Task AddCompanyAsync(CompanyDto model)
    {
        CheckModel(model);
        
        
        return  Task.CompletedTask;

    }

    public Task UpdateCompanyAsnyc(CompanyDto model)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteCompanyAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task UpDateCompanySettings(int companyId, IDictionary<string, string> settings)
    {
        throw new System.NotImplementedException();
    }

    public Task AddCompanySettings(int companyId, IDictionary<string, string> settings)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteCompanySettings(int companyId, IEnumerable<string> keys)
    {
        throw new System.NotImplementedException();
    }

    private void CheckModel(CompanyDto model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            throw new ArgumentNullException(nameof(model.Name));
        }
        if (string.IsNullOrWhiteSpace(model.Code))
        {
            throw new ArgumentNullException(nameof(model.Code));
        }
      
        
        
    }
}