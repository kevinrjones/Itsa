using System;

namespace ItsaWeb.Infrastructure
{
    public class NotLoggedInException : Exception
    {
        public NotLoggedInException()
            : base("NotLoggedInException")
        {

        }
    }
}