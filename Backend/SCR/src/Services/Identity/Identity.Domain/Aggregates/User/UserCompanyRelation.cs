using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Aggregates.User
{
    /// <summary>
    /// 人员绑定组织
    /// </summary>
    public class UserCompanyRelation
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
