using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dto
{
    public class UserDto
    {
        public string Account { get; set; }
        public int UserId { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }


    }
    public class UserProfileDto : UserDto { 
        public IEnumerable<RoleDto> Roles { get; set; }
    }

    public class UserListDto : UserDto { 
    
    }



}
