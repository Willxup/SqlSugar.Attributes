using System;

namespace SqlSugar.Attributes.Extension.Common
{
    /// <summary>
    /// 用户操作异常
    /// </summary>
    public class GlobalException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        public GlobalException() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        public GlobalException(string message) : base(message) { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public GlobalException(string message, Exception innerException) : base(message, innerException) { }
    }
}
