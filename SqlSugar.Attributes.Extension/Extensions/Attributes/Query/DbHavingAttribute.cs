using System;

namespace SqlSugar.Attributes.Extension.Extensions.Attributes.Query
{
    /// <summary>
    /// 数据库分组过滤条件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DbHavingAttribute : Attribute
    {
        /// <summary>
        /// 分组过滤条件
        /// </summary>
        private readonly string _condition;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="condition">分组过滤条件</param>
        public DbHavingAttribute(string condition)
        {
            _condition = condition;
        }

        /// <summary>
        /// 获取分组过滤条件
        /// </summary>
        /// <returns></returns>
        public string GetCondition()
        {
            return _condition;
        }
    }
}
