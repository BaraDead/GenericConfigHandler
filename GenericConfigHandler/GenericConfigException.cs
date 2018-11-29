using System;

namespace GenericConfigHandler
{
    public class GenericConfigException : Exception
    {
        public GenericConfigException(string message) : base(message)
        {}

        public GenericConfigException(string message, Exception innerException) : base(message, innerException)
        {}
    }
}