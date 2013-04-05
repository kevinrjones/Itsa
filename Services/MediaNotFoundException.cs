using System;
using System.Runtime.Serialization;

namespace Services
{
    [Serializable]
    public class MediaNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MediaNotFoundException()
        {
        }

        public MediaNotFoundException(string message) : base(message)
        {
        }

        public MediaNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MediaNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}