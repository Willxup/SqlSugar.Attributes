using System;

namespace SqlSugar.Attributes.Extension.Common
{
    /// <summary>
    /// 用户操作异常
    /// </summary>
    public class UserOperationException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserOperationException() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public UserOperationException(string message) : base(message) { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UserOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
