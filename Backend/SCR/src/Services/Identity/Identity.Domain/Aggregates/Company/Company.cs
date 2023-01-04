using Identity.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.Company
{
    public class Company : BaseEntity, IAggregateRoot
    {
        public int CompanyId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CompanySetting> Settings { get; set; }

        public Company(int companyId,string name) {
            this.CompanyId = companyId;
            this.Name = name;
            this.IsDeleted = false;
        }

    }

    
}
