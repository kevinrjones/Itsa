using System;
using System.Runtime.Serialization;

namespace ItsaWeb.Infrastructure
{
    [Serializable]
    public class NotLoggedInException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NotLoggedInException()
        {
        }

        public NotLoggedInException(string message) : base(message)
        {
        }

        public NotLoggedInException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotLoggedInException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}