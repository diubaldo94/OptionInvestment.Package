using System;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    [Serializable]
    internal class EndOfResultsException : Exception
    {
        public EndOfResultsException()
        {
        }

        public EndOfResultsException(string message) : base(message)
        {
        }

        public EndOfResultsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EndOfResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}