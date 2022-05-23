using Identity.Domain.Aggregates.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates
{
    public class Company : BaseEntity, IAggregateRoot
    {
        public int CompanyId { get; private set; }
        public string Name { get; private set; }
       
        

        public Company(int companyId,string name) {
            this.CompanyId = companyId;
            this.Name = name;
        }

    }

    
}
