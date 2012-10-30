using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceInterfaces;

namespace Services
{
    public class SessionService : ISessionService
    {
        public bool Logon(string userName, string password)
        {
            return true; // todo: logon?
        }
    }
}
