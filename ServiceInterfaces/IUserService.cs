using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceInterfaces
{
    public interface IUserService
    {
        void Register(string userName, string password, string email);
        bool UnRegister();
        bool Logon(string userName, string password);
        User GetRegisteredUser();
    }

}
