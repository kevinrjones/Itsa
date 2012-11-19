using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceInterfaces;

namespace Services
{
    public class SessionService : ISessionService
    {
        public bool Register(User user)
        {
            throw new NotImplementedException();
        }

        public bool Logon(string userName, string password)
        {
            return true; // todo: logon?
        }
    }
}
