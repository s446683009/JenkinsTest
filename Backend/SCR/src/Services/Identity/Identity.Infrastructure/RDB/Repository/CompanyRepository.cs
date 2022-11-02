using Identity.Domain.Aggregates.Company;
using Identity.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.RDB.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IdentityContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CompanyRepository(IdentityContext context) {
            _context = context;
        }
        public async Task<IList<Company>> GetCompaniesByIdsAsync(IList<int> companyIds)
        {
          return await _context.Companys.Where(t=>t.IsDeleted==false&&companyIds.Contains(t.CompanyId)).ToListAsync();
        }

        public async Task AddCompanyAsync(Company company)
        {
             await _context.Companys.AddAsync(company);
        }
    }
}
