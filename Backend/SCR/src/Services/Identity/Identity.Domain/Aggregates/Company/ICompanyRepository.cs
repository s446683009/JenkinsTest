using Identity.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Company
{
    public interface ICompanyRepository:IRepository<Company>
    {
       Task<IList<Company>> GetCompaniesByIdsAsync(IList<int> companyIds);
       Task AddCompanyAsync(Company company);

       Task DeleteAsync(Company company);

       Task<Company> FindById(int companyId);
        
       
       
    }
}
