using System;

namespace SqlSugar.Attributes.Extension.Extensions.Attributes.Query
{
    /// <summary>
    /// 数据库查询特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbQueryAttribute : Attribute
    {
    }
}
