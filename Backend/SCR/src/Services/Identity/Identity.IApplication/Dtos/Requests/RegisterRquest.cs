using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.IApplication.Dtos.Requests
{
    public class RegisterRequest
    {

        public RegisterRequest() {
            CompanyIds = new List<int>();
            RoleIds = new List<int>();
        }
        public string Account { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public int DefaultCompanyId { get; set; }
        public IList<int> CompanyIds { get; set; }
        public IList<int> RoleIds { get; set; }
    }
}
