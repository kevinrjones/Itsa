﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ItsaWeb.Authentication
{
    interface IItsaIdentity : IIdentity
    {
        string UserId { get; }
    }
}
