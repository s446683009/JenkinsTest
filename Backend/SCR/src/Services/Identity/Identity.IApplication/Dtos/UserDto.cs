using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UserDto
    {
        public string Account { get; set; }
        public int UserId { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public  bool Actived { get; set; }

    }
    public class UserProfileDto : UserDto {
        public UserProfileDto()
        {
            this.Roles = new List<RoleDto>();
        }

        public IEnumerable<RoleDto> Roles { get; set; }
    }

    public class UserListDto : UserDto { 
    
    }



}
