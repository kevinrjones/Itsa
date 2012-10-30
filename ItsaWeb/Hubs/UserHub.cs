using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItsaWeb.Models;

namespace ItsaWeb.Hubs
{
    public class UserHub : AuthenticatingHub
    {
        public UserViewModel GetUserName()
        {
            try
            {
                var model = new UserViewModel {UserName = UserName, AllowComments = true};
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}