﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IdentityApplication
    {
        Task UserLogin(string userName,string password);
    }
}
