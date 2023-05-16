using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Identity.Application.Dtos.Requests
{
    public class UserSearchRequest:PageSearchRequest
    {

        public string UserName { get; set; }

        public int? CompanyId { get; set; }
  
    }

    public class UserSearchValidator : AbstractValidator<UserSearchRequest>
    {
        public UserSearchValidator()
        {
            RuleFor(t => t.Page).GreaterThan(0);
        }
        
        
    }
}
