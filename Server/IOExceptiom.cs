using System;
using System.Runtime.Serialization;

namespace Server
{
    [Serializable]
    internal class IOExceptiom : Exception
    {
        public IOExceptiom()
        {
        }

        public IOExceptiom(string message) : base(message)
        {
        }

        public IOExceptiom(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IOExceptiom(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}