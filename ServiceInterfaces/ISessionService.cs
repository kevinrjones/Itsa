using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceInterfaces
{
    public interface ISessionService
    {
        bool Register(User user);
        bool Logon(string userName, string password);
    }
}
