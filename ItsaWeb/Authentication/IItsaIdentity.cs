using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ItsaWeb.Authentication
{
    public interface IItsaIdentity : IIdentity
    {
        string Name { get; set; }
        string AuthenticationType { get; set; }
        bool IsAuthenticated { get;  set; }
        
    }
}
