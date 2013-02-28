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
        User Register(string email, string password);
        bool UnRegister();
        User Logon(string email, string password);
        User GetRegisteredUser();
        User GetUser();
    }

}
