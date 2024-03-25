using System;

namespace SqlSugar.Attributes.Extension.Common
{
    public class UserOperationException : Exception
    {
        public UserOperationException() { }

        public UserOperationException(string message) : base(message) { }

        public UserOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
