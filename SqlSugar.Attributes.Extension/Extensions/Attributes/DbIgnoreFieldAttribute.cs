using System;

namespace SqlSugar.Attributes.Extension.Extensions
{
    /// <summary>
    /// 忽略字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbIgnoreFieldAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DbIgnoreFieldAttribute() { }
    }
}
