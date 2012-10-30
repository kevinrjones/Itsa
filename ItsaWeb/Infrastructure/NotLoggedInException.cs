using System;

namespace ItsaWeb.Hubs
{
    public class NotLoggedInException : Exception
    {
        public NotLoggedInException()
            : base("NotLoggedInException")
        {

        }
    }
}