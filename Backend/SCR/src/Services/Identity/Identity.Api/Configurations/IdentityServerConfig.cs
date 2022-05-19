using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Configurations
{
    public class IdentityServerConfig
    {

        public UserInteractionSetting UserInteraction { get; set; }
    }

    public class UserInteractionSetting { 
        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }

        public string LoginReturnUrlParameter { get;set; }


    }
}
